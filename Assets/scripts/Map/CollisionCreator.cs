using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollisionCreator : MonoBehaviour
{
    
    private static readonly string COLLISION_OBJECT_NAME = "AUTO_COLLISION";

    public bool CreateShadowCaster = true;
    public bool SelfShadows = true;
    
    private Tilemap _map;

    public void Start()
    {
        _map = GetComponent<Tilemap>();
        if (_map == null){
            Debug.LogError($"{name} : can not add CollisionCreator on object without a tilemap");
            return;
        }
        var collisionObject = CreateCollider();
        if (CreateShadowCaster){
            collisionObject.AddComponent<ShadowCaster2DCreator>().CreateShadowCaster(SelfShadows);
        }
    }

    private GameObject CreateCollider()
    {
        var collision = new GameObject(COLLISION_OBJECT_NAME, typeof(Tilemap), typeof(TilemapCollider2D), typeof(CompositeCollider2D));
        collision.transform.SetParent(transform.parent, false);
        collision.GetComponent<TilemapCollider2D>().usedByComposite = true;
        collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        var tilemap = collision.GetComponent<Tilemap>();
        var basetile = GetFirstTileBase();

        var lowerLeftCorner = _map.GetLowerLeftCorner();
        

        for (int x = lowerLeftCorner.x - 2; x < _map.cellBounds.xMax + 2; x++)
        {
            for (int y = lowerLeftCorner.y - 2; y < _map.cellBounds.yMax + 2; y++)
            {
                var pos = new Vector3Int(x, y, 0);
                if(!_map.HasTile(pos))
                    tilemap.SetTile(pos, basetile);
            }
        }

        return collision;
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

/*
[CustomEditor(typeof(CollisionCreator)), CanEditMultipleObjects]
public class CollisionCreatorEditor : Editor
{

    private CollisionCreator _target;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (CollisionCreator)target;
        
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Create/Update collider"))
        {
            _target.Start();
        }

        EditorGUILayout.EndHorizontal();
    }

}
*/