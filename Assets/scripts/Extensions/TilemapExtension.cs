using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class TilemapExtension
{
    public static Vector2Int GetLowerLeftCorner(this Tilemap map)
    {
        int minX = Int32.MaxValue;
        int minY = Int32.MaxValue;
        foreach (var pos in map.cellBounds.allPositionsWithin)
        {
            if(map.HasTile(pos)){
                minX = Math.Min(minX, pos.x);
                minY = Math.Min(minY, pos.y);
            }
        }

        return new Vector2Int(minX, minY);
    }
}