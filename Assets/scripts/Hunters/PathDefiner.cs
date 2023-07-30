using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PathDefiner : MonoBehaviour
{

    public bool Loop = true;
    public PathPoint Target => _points[_targetIndex];

    private PathPoint[] _points;
    private int _targetIndex;
    private bool _forward = true;
    private int DiffIndex => Loop || _forward ? 1 : -1;


    private void Start(){
        _points = Enumerable.Range(0, transform.childCount)
            .Select(index => transform.GetChild(index).GetComponent<PathPoint>())
            .ToArray();

    }

    public void NextTarget(){
        if (!Loop){
            if ((_forward && _targetIndex == _points.Length - 1) || (!_forward && _targetIndex == 0))
                _forward = !_forward;
        }
        else if (_targetIndex == _points.Length - 1)
            _targetIndex = -1;
        _targetIndex += DiffIndex;
    }

    
    void OnDrawGizmos()
    {
        for (int i = 1; i < transform.childCount; i++){
            var p1 = transform.GetChild(i - 1).transform.position;
            var p2 = transform.GetChild(i).transform.position;
            Debug.DrawLine(p1, p2, Color.yellow);
        }

        if (Loop && transform.childCount > 2){
            var firstPoint = transform.GetChild(0).transform.position;
            var lastPoint = transform.GetChild(transform.childCount - 1).transform.position;
            Debug.DrawLine(firstPoint, lastPoint, Color.yellow);
        }
    }

    private void OnDrawGizmosSelected(){
        for (int i = 0; i < transform.childCount; i++){
            var p1 = transform.GetChild(i);
            Handles.Label(p1.position, p1.name);
        }
    }
    
}



[CustomEditor(typeof(PathDefiner)), CanEditMultipleObjects]
public class PathCreatorEditor : Editor
{
    private PathDefiner _target;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (PathDefiner)target;
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Add point to path"))
            AddPointToPath();
        if(GUILayout.Button("Rename points"))
            RenamePoints();

        EditorGUILayout.EndHorizontal();
    }

    private void AddPointToPath(){
        var PathPointPrefan = GetPointPrefab();
        var path = _target.GameObject();
        var point = PrefabUtility.InstantiatePrefab(PathPointPrefan) as GameObject;
        point.transform.SetParent(path.transform);
        point.name = $"point_{path.transform.childCount}";
        if (path.transform.childCount > 1)
            point.transform.position = path.transform.GetChild(point.transform.GetSiblingIndex() - 1).position
                                       + Vector3.right;
        else
            point.transform.localPosition = Vector3.zero;
    }

    private GameObject GetPointPrefab(){
        return Resources.Load<GameObject>("PathPoint");
    }

    private void RenamePoints(){
        var path = _target.GameObject().transform;
        for (int i = 0; i < path.childCount; i++){
            path.GetChild(i).name = $"point_{i+1}";
        }
    }


}