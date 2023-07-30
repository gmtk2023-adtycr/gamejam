using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Grid
{

    private Node[,] grid;
    private readonly Tilemap _map;
    private readonly int _gridSizeX;
    private readonly int _gridSizeY;
    private Vector2Int _mapLowerLeftCorner;

    private static List<Vector2Int> DIRECTIONS = new List<Vector2Int>()
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

    public Grid(Tilemap map){
        _map = map;
        _mapLowerLeftCorner = _map.GetLowerLeftCorner();
        _gridSizeX = _map.cellBounds.xMax - _mapLowerLeftCorner.x;
        _gridSizeY = _map.cellBounds.yMax - _mapLowerLeftCorner.y;
        CreateGrid();
    }
	

    void CreateGrid() {
        grid = new Node[_gridSizeX,_gridSizeY];
        var worldBottomLeft = _map.GetLowerLeftCorner();

        for (int x = 0; x < _gridSizeX; x ++) {
            for (int y = 0; y < _gridSizeY; y ++){
                var pos = new Vector3Int(_mapLowerLeftCorner.x + x, _mapLowerLeftCorner.y + y);
                grid[x,y] = new Node(pos, _map.HasTile(pos), x, y);
            }
        }
    }

    private Vector2Int WorldPosToGridIndex(Vector3 worldPos){
        var tileMapPos = _map.WorldToCell(worldPos);
        return new Vector2Int(tileMapPos.x - _mapLowerLeftCorner.x, tileMapPos.y - _mapLowerLeftCorner.y);
    }
    
    public Node NodeFromWorldPoint(Vector3 worldPosition){
        var gridIndexes = WorldPosToGridIndex(worldPosition);
        return grid[gridIndexes.x,gridIndexes.y];
    }

    public Vector3 WorldPosFromNode(Node node){
        var x = node.GridX + _mapLowerLeftCorner.x;
        var y = node.GridY + _mapLowerLeftCorner.y;
        return _map.CellToWorld(new Vector3Int(x, y));
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
                cutCorner = grid[node.GridX, node.GridY + direction.y].Walkable
                            && grid[node.GridX + direction.x, node.GridY].Walkable;

            if (cutCorner)
                neighbours.Add(grid[checkX, checkY]);
        }


        return neighbours;
    }

    public bool OutOfBound(Vector3 startPos, Vector3 targetPos){
        return !(_map.HasTile(_map.WorldToCell(startPos)) && _map.HasTile(_map.WorldToCell(targetPos)));
    }
}