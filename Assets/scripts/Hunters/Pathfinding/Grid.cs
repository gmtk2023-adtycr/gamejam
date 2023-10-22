using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class Grid : MonoBehaviour
{

    private static readonly List<Vector2Int> DIRECTIONS = new List<Vector2Int>()
    {
        Vector2Int.up,
        Vector2Int.up + Vector2Int.right,
        Vector2Int.right,
        Vector2Int.down + Vector2Int.right,
        Vector2Int.down,
        Vector2Int.down + Vector2Int.left,
        Vector2Int.left,
        Vector2Int.up + Vector2Int.left
    };

    private static List<Grid> Grids = new ();

    private List<Node> _nodes;
    private Tilemap _map;
    public int MaxSize => _gridSizeX * _gridSizeY;
    private int _gridSizeX;
    private int _gridSizeY;
    private Vector2Int _mapLowerLeftCorner;

    // Testing
    public int NodePerTile { get; set; } = 5;
    public bool DrawGrid;
    public Transform StartPos, EndPos;


    public static Grid GetGridForPos(Vector3 pos){
        return Grids.First(g =>
            g._nodes.First().Position.x <= pos.x &&
            g._nodes.First().Position.y <= pos.y &&
            g._nodes.Last().Position.x >= pos.x &&
            g._nodes.Last().Position.y >= pos.y
        );
    }

    public void Awake(){
        Grids.Add(this);
        _map = GetComponent<Tilemap>();
        _mapLowerLeftCorner = _map.GetLowerLeftCorner() + new Vector2Int(-2, -2);
        _gridSizeX = _map.cellBounds.xMax - _mapLowerLeftCorner.x;
        _gridSizeY = _map.cellBounds.yMax - _mapLowerLeftCorner.y;
        _gridSizeX *= NodePerTile;
        _gridSizeY *= NodePerTile;
        CreateGrid();        
        CreatePenalties();
    }



    void CreateGrid() {
        _nodes = new();
        var meubles = GameObject.FindObjectsOfType<BoxCollider2D>()
            .Where(collider => collider.GetComponent<UsableItem>() == null)
            .ToList();
        var nodeWidth = 1f / NodePerTile;

        for (int y = 0; y < _gridSizeY; y ++){
            for (int x = 0; x < _gridSizeX; x ++) {
                var tilePos = new Vector3Int(
                    _mapLowerLeftCorner.x + x / NodePerTile, 
                    _mapLowerLeftCorner.y + y / NodePerTile
                );
                float dx = (float) (x % NodePerTile) / NodePerTile;
                float dy = (float) (y % NodePerTile) / NodePerTile;
                var worldPos = new Vector3(tilePos.x + dx, tilePos.y + dy) 
                               + _map.transform.position
                               + (Vector3.up + Vector3.right) * 0.5f / NodePerTile;
                var walkable = _map.HasTile(tilePos);
                walkable &= !meubles.Any(m => m.OverlapPoint(new Vector2(worldPos.x + nodeWidth, worldPos.y + nodeWidth)));
                _nodes.Add(new Node(worldPos, walkable, x, y));
            }
        }
    }

    private void CreatePenalties(){
        var nodesToProcess = _nodes.Where(n => n.Walkable).ToList();
        var penalties = Enumerable.Range(1, 20)
            .Select(x => Math.Pow(x, 1.5f) * 10)
            .Reverse()
            .ToList();
        int penaltyIndex = 0;
        
        while (nodesToProcess.Any()){
            var nodesPenalty = nodesToProcess.Where(node => {
                var neighbours = GetNeighbours(node);
                bool nextToWall = neighbours.Any(n => !n.Walkable);
                bool atEdge = node.GridX == 0 || node.GridY == 0 || node.GridX == _gridSizeX - 1 ||
                              node.GridY == _gridSizeY - 1;
                bool nextToNodeWithPenalty = neighbours.Any(n => n.Penalty > 0);
                return nextToWall || atEdge || nextToNodeWithPenalty;
            }).ToList();
            nodesPenalty.ForEach(n => n.Penalty = (int)penalties[penaltyIndex]);
            penaltyIndex = Math.Min(penaltyIndex + 1, penalties.Count - 1);
            nodesToProcess.RemoveAll(n => nodesPenalty.Contains(n));
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition){
        return _nodes.OrderBy(node => Vector3.Distance(worldPosition, node.Position))
            .First();
    }
    
    public List<Node> GetNeighbours(Node node){
        List<Node> neighbours = new List<Node>();

        foreach (var direction in DIRECTIONS){
            int checkX = node.GridX + direction.x;
            int checkY = node.GridY + direction.y;
            
            if(checkX < 0 || checkX >= _gridSizeX || checkY < 0 || checkY >= _gridSizeY)
                continue;
            

            bool cutCorner = true;
            if (Math.Abs(direction.x) + Math.Abs(direction.y) == 2) //diagonal
                cutCorner = GetNode(node.GridX, node.GridY + direction.y).Walkable
                            && GetNode(node.GridX + direction.x, node.GridY).Walkable;

            if (cutCorner)
                neighbours.Add(GetNode(checkX, checkY));
        }


        return neighbours;
    }

    private Node GetNode(int x, int y){
        return _nodes[x + y * _gridSizeX];
    }

    public bool OutOfBound(Vector3 startPos, Vector3 targetPos){
        return !(_map.HasTile(_map.WorldToCell(startPos)) && _map.HasTile(_map.WorldToCell(targetPos)));
    }


    private void OnDrawGizmos(){
        if (_map == null || _nodes == null)
            Awake();

        if(!DrawGrid || _nodes == null) return;

        var startNode = StartPos != null ? NodeFromWorldPoint(StartPos.position) : null;
        var endNode =  EndPos != null ? NodeFromWorldPoint(EndPos.position) : null;

        List<Node> path = new();
        if (startNode != null && endNode != null){
            path = PathFinding.FindPath(this, StartPos.position, EndPos.position);
        }
        

        foreach (var node in _nodes){
            if(node == startNode)
                Gizmos.color = Color.green;
            else if(node == endNode)
                Gizmos.color = Color.red;
            else if(path.Contains(node))
                Gizmos.color = Color.magenta;
            else
                Gizmos.color = node.Walkable ? Color.Lerp(Color.blue, Color.cyan, node.fCost / 150f) : Color.black;
            Gizmos.DrawSphere(node.Position, .5f / NodePerTile);
        }
    }

}

[CustomEditor(typeof(Grid)), CanEditMultipleObjects]
class GridEditor : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        var grid = (Grid) target;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PrefixLabel("Nodes per tile");
        int newNodePerTile = (int)EditorGUILayout.Slider(grid.NodePerTile, 1, 6);
        if (newNodePerTile != grid.NodePerTile){
            grid.NodePerTile = newNodePerTile;
            grid.Awake();
        }
        EditorGUILayout.EndHorizontal();

    }
}