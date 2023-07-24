using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class RoomDefiner : MonoBehaviour
{

    private Tilemap _map;
    private Vector3 _exploredPosition;
    private List<Vector3Int> _tiles = new();
    
    private static readonly List<(int, int)> Directions = new()
    {
        (1, 0),
        (0, -1),
        (-1, 0),
        (0, 1)
    };
    
    void Initialize(){
        _map = transform.GetComponentInParent<Tilemap>();
        if (_map == null){
            Debug.LogError("RoomDefiner must be child of a tilemap");
            return;
        }
        if(_exploredPosition.Equals(transform.position))
            return;
        ExploreRoom();
    }
    
    private void CreateCollider(){
        var collider = gameObject.GetComponent<PolygonCollider2D>();
        if(collider == null)
            collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.points = ContourShape().Select(v3 => (Vector2) (v3 - gameObject.transform.position)).ToArray();
    }

    private void RemoveCollider(){
        DestroyImmediate(gameObject.GetComponent<PolygonCollider2D>());
    }

    private void ExploreRoom(){
        _tiles = new List<Vector3Int>();
        _exploredPosition = transform.position;
        
        var startingCell = _map.WorldToCell(transform.position);
        if(!_map.HasTile(startingCell))
            return;

        _tiles.Add(startingCell);
        var sprite = _map.GetSprite(startingCell);
        
        ExploreNeighbours(startingCell, sprite);
    }

    private void ExploreNeighbours(Vector3Int position, Sprite sprite){
        foreach (var (dx, dy) in Directions){
            var tile = new Vector3Int(dx, dy, 0) + position;
            if(_map.GetSprite(tile) != sprite || _tiles.Any(t => t.Equals(tile)))
                continue;
            _tiles.Add(tile);
            ExploreNeighbours(tile, sprite);
        }
    }

    public List<Vector3> ContourShape(){
        var points = new List<Vector2Int>();
        if (_tiles.Count == 0)
            return new List<Vector3>();

        var minX = _tiles.Select(t => t.x).Min() - 1;
        var minY = _tiles.Select(t => t.y).Min() - 1;
        var maxX = _tiles.Select(t => t.x).Max() + 1;
        var maxY = _tiles.Select(t => t.y).Max() + 1;

        // contouring algo
        for (int x = minX; x <= maxX; x++){
            for (int y = minY; y <= maxY; y++){
                var d_in_square = _tiles.Contains(new Vector3Int(x, y, 0));
                var d_in_square_on_the_left = _tiles.Contains(new Vector3Int(x-1, y, 0));
                var d_in_square_above = _tiles.Contains(new Vector3Int(x, y+1, 0));

                // draw a horizontal piece if there is a sign change
                if( d_in_square != d_in_square_above) {
                    points.Add(new Vector2Int(x, y+1));
                    points.Add(new Vector2Int(x+1, y+1));
                }

                // draw a vertical piece if there is a sign change
                if(d_in_square != d_in_square_on_the_left) {
                    points.Add(new Vector2Int(x, y));
                    points.Add(new Vector2Int(x, y+1));
                }
            }
        }

        //distinct des points
        var unordered_points = points
            .DistinctBy(t => $"{t.x},{t.y}")
            .ToList();
        
        // on remet les points dans l'ordre
        points.Clear();
        points.Add(unordered_points[0]);
        unordered_points.RemoveAt(0);
        while(unordered_points.Count > 0){
            var point = unordered_points.OrderBy(p => (points.Last() - p).magnitude).FirstOrDefault();
            points.Add(point);
            unordered_points.Remove(point);
        }

        Vector2Int direction = Vector2Int.zero;
        
        for (int i = 1; i < points.Count; i++){
            var new_dir = points[i] - points[i - 1];
            if (new_dir.Equals(direction)){
                points.RemoveAt(--i);
            }
            direction = new_dir;
        }

        points.Remove(points.Last());

        return points
            .Select(p => _map.CellToWorld(new Vector3Int(p.x, p.y, 0)))
            .ToList();
    }


    private void OnDrawGizmos(){
        Handles.Label(transform.position, name);
    }

    private void OnDrawGizmosSelected(){
        Initialize();

        var points = ContourShape();
        for(int i = 1; i <= points.Count; i++){
            var p1 = points[i-1];
            var p2 = points[i%points.Count];
            Debug.DrawLine(p1, p2, Color.red);
        }
    }
}


[CustomEditor(typeof(RoomDefiner)), CanEditMultipleObjects]
public class RoomDefinerEditor : Editor
{

    private GameObject gameObject;
    private RoomDefiner _target;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (RoomDefiner)target;
        gameObject = _target.gameObject;
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Create/Update collider"))
            CreateCollider();
        if (GUILayout.Button("Remove collider"))
            RemoveCollider();


        EditorGUILayout.EndHorizontal();
    }

    private void CreateCollider(){
        var collider = gameObject.GetComponent<PolygonCollider2D>();
        if(collider == null)
            collider = gameObject.AddComponent<PolygonCollider2D>();
        collider.points = _target.ContourShape().Select(v3 => (Vector2) (v3 - gameObject.transform.position)).ToArray();
        collider.isTrigger = true;
    }

    private void RemoveCollider(){
        DestroyImmediate(gameObject.GetComponent<PolygonCollider2D>());
    }
    

}

