using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapExplorer
{

    private readonly Tilemap _map;
    private List<Vector3Int> _lastCalculatedTiles;

    public delegate bool Propagation(Tilemap map, Vector3Int pos1, Vector3Int pos2);    
    public TilemapExplorer(Tilemap map)
    {
        _map = map;
    }
    
    private static readonly List<(int, int)> Directions = new()
    {
        (1, 0),
        (0, -1),
        (-1, 0),
        (0, 1)
    };
    
    public List<Vector3Int> ExploreSpace(Vector3 position, Propagation comparator, bool forceUpdate = false)
    {
        HashSet<Vector3Int> tiles = new ();
        
        var startingCell = _map.WorldToCell(position);
        if(!_map.HasTile(startingCell))
            return tiles.ToList();

        tiles.Add(startingCell);
        
        ExploreNeighbours(startingCell, tiles, comparator);
        _lastCalculatedTiles = tiles.ToList();
        return _lastCalculatedTiles;
    }

    private void ExploreNeighbours(Vector3Int position, HashSet<Vector3Int> tiles, Propagation comparator){
        foreach (var (dx, dy) in Directions){
            var tile = new Vector3Int(dx, dy, 0) + position;
            if(comparator(_map, position, tile) || tiles.Contains(tile))
                continue;
            tiles.Add(tile);
            ExploreNeighbours(tile, tiles, comparator);
        }
    }
    
    

    public List<Vector3> ContourShape(List<Vector3Int> tiles){
        var points = new List<Vector2Int>();
        if (tiles.Count == 0)
            return new List<Vector3>();

        var minX = tiles.Select(t => t.x).Min() - 1;
        var minY = tiles.Select(t => t.y).Min() - 1;
        var maxX = tiles.Select(t => t.x).Max() + 1;
        var maxY = tiles.Select(t => t.y).Max() + 1;

        // contouring algo
        for (int x = minX; x <= maxX; x++){
            for (int y = minY; y <= maxY; y++){
                bool dInSquare = tiles.Contains(new Vector3Int(x, y, 0));
                bool dInSquareOnTheLeft = tiles.Contains(new Vector3Int(x-1, y, 0));
                bool dInSquareAbove = tiles.Contains(new Vector3Int(x, y+1, 0));

                // draw a horizontal piece if there is a sign change
                if( dInSquare != dInSquareAbove) {
                    points.Add(new Vector2Int(x, y+1));
                    points.Add(new Vector2Int(x+1, y+1));
                }

                // draw a vertical piece if there is a sign change
                if(dInSquare != dInSquareOnTheLeft) {
                    points.Add(new Vector2Int(x, y));
                    points.Add(new Vector2Int(x, y+1));
                }
            }
        }

        //distinct des points
        var unorderedPoints = points
            .DistinctBy(t => $"{t.x},{t.y}")
            .ToList();
        
        // tri des points
        points.Clear();
        points.Add(unorderedPoints[0]);
        unorderedPoints.RemoveAt(0);
        while(unorderedPoints.Count > 0){
            var point = unorderedPoints.OrderBy(p => (points.Last() - p).magnitude).FirstOrDefault();
            points.Add(point);
            unorderedPoints.Remove(point);
        }

        // suppression des points superflus
        Vector2Int direction = Vector2Int.zero;
        for (int i = 1; i < points.Count; i++){
            var newDir = points[i] - points[i - 1];
            if (newDir.Equals(direction)){
                points.RemoveAt(--i);
            }
            direction = newDir;
        }
        points.Remove(points.Last());

        return points
            .Select(p => _map.CellToWorld(new Vector3Int(p.x, p.y, 0)))
            .ToList();
    }
    
    
}