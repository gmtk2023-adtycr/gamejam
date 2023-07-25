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
    private TilemapExplorer _tilemapExplorer;

    void Awake()
    {
        if(_tilemapExplorer != null) return;
        
        _map = transform.GetComponentInParent<Tilemap>();
        if (_map == null){
            Debug.LogError("RoomDefiner must be child of a tilemap");
            return;
        }
        _tilemapExplorer = new TilemapExplorer(_map);
    }

    private List<Vector3Int> ExploreRoom()
    {
        return _tilemapExplorer.ExploreSpace(transform.position, (m, t1, t2) => m.GetSprite(t1) != m.GetSprite(t2));
    }

    public void CreateCollider()
    {
        var collider = GetComponent<PolygonCollider2D>();
        if (collider == null)
            collider = gameObject.AddComponent<PolygonCollider2D>();
        var coveredTiles = ExploreRoom();
        collider.points = _tilemapExplorer.ContourShape(coveredTiles).Select(v3 => (Vector2)(v3 - transform.position)).ToArray();
    }

    public void RemoveCollider() {
        DestroyImmediate(gameObject.GetComponent<PolygonCollider2D>());
    }


    private void OnDrawGizmos(){
        Handles.Label(transform.position, name);
    }

    private void OnDrawGizmosSelected(){
        Awake();
        var coveredTiles = ExploreRoom();
        var points = _tilemapExplorer.ContourShape(coveredTiles);
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
            _target.CreateCollider();
        if (GUILayout.Button("Remove collider"))
            _target.RemoveCollider();


        EditorGUILayout.EndHorizontal();
    }


}

