using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HunterMovement))]
public class FollowPath : MonoBehaviour
{

    private static string PATH_PARENT_NAME = "Path";
    private static float DISTANCE_THRESHOLD = 0.5f;

    private HunterMovement _movement;
    private PathDefiner _path;

    private bool _waiting;

    void Start(){
        _movement = GetComponent<HunterMovement>();
        _path = GetPath();
        _path.Initialize();
        if (_path.transform.childCount == 0){
            enabled = false;
            return;
        }
        _path.transform.parent = transform.parent;
        Invoke(nameof(GoToTarget), 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_waiting || Vector3.Distance(transform.position, _path.Target.transform.position) > DISTANCE_THRESHOLD)
            return;

        if (_path.Target.waitingTime > 0f){
            _waiting = true;
            _movement.Stop();
            Invoke(nameof(StopWaiting), _path.Target.waitingTime);
        }
        _path.NextTarget();
        GoToTarget();

    }

    public void GoToTarget()
    {
        _movement.WalkToPos(_path.Target.transform.position);
    }

    private void StopWaiting(){
        _waiting = false;
        GoToTarget();
    }

    private PathDefiner GetPath(){
        for (int i = 0; i < transform.parent.childCount; i++){
            var obj = transform.parent.GetChild(i).GameObject();
            if (obj.name == PATH_PARENT_NAME)
                return obj.GetComponent<PathDefiner>();
        }

        throw new Exception($"Hunter {name} has no path defined");
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.cyan;
        if(_path != null)
            Gizmos.DrawSphere(_path.Target.transform.position, DISTANCE_THRESHOLD / 2f);
    }
}
