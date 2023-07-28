using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(HunterMovement))]
public class FollowPath : MonoBehaviour
{

    public static string PATH_PARENT_NAME = "Path";

    
    public float speed = 3;

    private bool _waiting;
    private int _targetIndex;
    private PathPoint _target => _path.transform.GetChild(_targetIndex).GetComponent<PathPoint>();
    private GameObject _path;
    private HunterMovement _movement;
    
    void Start(){
        _path = GetPathParent();
        _movement = GetComponent<HunterMovement>();
        if (_path.transform.childCount == 0){
            enabled = false;
            return;
        }
        _path.transform.parent = transform.parent;
        GoToTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (_waiting || _target == null)
            return;
        
        if (Vector3.Distance(transform.position, _target.transform.position) < 0.3f){
            if (_target.waitingTime > 0f){
                _waiting = true;
                _movement.Stop();
                Invoke(nameof(StopWaiting), _target.waitingTime);
            }
            _targetIndex = (_targetIndex + 1) % _path.transform.childCount;
        }
        else
            GoToTarget();

    }

    private void GoToTarget()
    {
        _movement.WalkToPos(_target.transform.position);
    }

    private void StopWaiting(){
        _waiting = false;
        GoToTarget();
    }
    
    public GameObject GetPathParent(){
        for (int i = 0; i < transform.childCount; i++){
            var obj = transform.GetChild(i).GameObject();
            if (obj.name == PATH_PARENT_NAME)
                return obj;
        }

        var pathParent = new GameObject(PATH_PARENT_NAME);
        pathParent.transform.parent = transform;
        return pathParent;
    }
    
}




[CustomEditor(typeof(FollowPath)), CanEditMultipleObjects]
public class FollowPathEditor : Editor
{
    private FollowPath _target;
    private GameObject _gameObject;
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (FollowPath)target;
        _gameObject = _target.GameObject();
        EditorGUILayout.BeginHorizontal();
        
        if (GUILayout.Button("Add point to path"))
            AddPointToPath();
        if(GUILayout.Button("Rename points"))
            RenamePoints();

        EditorGUILayout.EndHorizontal();
    }

    protected void AddPointToPath(){
        var PathPointPrefan = GetPointPrefab();
        var path = _target.GetPathParent();
        var point = PrefabUtility.InstantiatePrefab(PathPointPrefan) as GameObject;
        point.transform.parent = path.transform;
        point.name = $"point_{path.transform.childCount}";
        if (path.transform.childCount > 1)
            point.transform.position = path.transform.GetChild(point.transform.GetSiblingIndex() - 1).position
                                       + Vector3.right;
    }

    private GameObject GetPointPrefab(){
        return Resources.Load<GameObject>("PathPoint");
    }

    private void RenamePoints(){
        var path = _target.GetPathParent().transform;
        for (int i = 0; i < path.childCount; i++){
            path.GetChild(i).name = $"point_{i+1}";
        }
    }


}
