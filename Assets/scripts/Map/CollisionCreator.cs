using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionCreator : MonoBehaviour
{
    private Tilemap _map;

    private static readonly string COLLISION_OBJECT_NAME = "AUTO_COLLISION";

    public void Start()
    {
        _map = GetComponent<Tilemap>();
        if (_map == null)
            Debug.LogError($"{name} : can not add CollisionCreator on object without a tilemap");
        else
            CreateCollider();
    }

    public void CreateCollider()
    {
        var collision = new GameObject(COLLISION_OBJECT_NAME, typeof(Tilemap), typeof(TilemapCollider2D), typeof(CompositeCollider2D));
        collision.transform.SetParent(transform.parent);
        collision.GetComponent<TilemapCollider2D>().usedByComposite = true;
        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        var tilemap = collision.GetComponent<Tilemap>();
        var basetile = GetFirstTileBase();

        var lowerLeftCorner = GetLowerLeftCorner();
        

        for (int x = lowerLeftCorner.x - 2; x < _map.cellBounds.xMax + 2; x++)
        {
            for (int y = lowerLeftCorner.y - 2; y < _map.cellBounds.yMax + 2; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                if(!_map.HasTile(pos))
                    tilemap.SetTile(pos, basetile);
            }
        }
    }

    private Vector2Int GetLowerLeftCorner()
    {
        int minX = Int32.MaxValue;
        int minY = Int32.MaxValue;
        foreach (var pos in _map.cellBounds.allPositionsWithin)
        {
            if(_map.HasTile(pos)){
                minX = Math.Min(minX, pos.x);
                minY = Math.Min(minY, pos.y);
            }
        }

        return new Vector2Int(minX, minY);
    }

    private TileBase GetFirstTileBase()
    {
        foreach (var pos in _map.cellBounds.allPositionsWithin)
        {
            var tBase =_map.GetTile(pos);
            if (tBase != null)
                return tBase;
        }
        throw new Exception("??");
    }
}

[CustomEditor(typeof(CollisionCreator)), CanEditMultipleObjects]
public class CollisionCreatorEditor : Editor
{

    private CollisionCreator _target;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (CollisionCreator)target;
        
        return;//for debug
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create/Update collider"))
        {
            _target.Start();
            _target.CreateCollider();

        }

        EditorGUILayout.EndHorizontal();
    }


}