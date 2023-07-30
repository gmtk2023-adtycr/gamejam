using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(HunterMovement))]
public class FollowPath : MonoBehaviour
{

    public static string PATH_PARENT_NAME = "Path";

    private HunterMovement _movement;
    private PathDefiner _path;

    private bool _waiting;

    void Start(){
        _movement = GetComponent<HunterMovement>();
        _path = GetPath();
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
        if (_waiting)
            return;
        
        if (Vector3.Distance(transform.position, _path.Target.transform.position) < 0.1f){
            if (_path.Target.waitingTime > 0f){
                _waiting = true;
                _movement.Stop();
                Invoke(nameof(StopWaiting), _path.Target.waitingTime);
            }
            _path.NextTarget();
        }
        else
            GoToTarget();

    }

    private void GoToTarget()
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
    
}
