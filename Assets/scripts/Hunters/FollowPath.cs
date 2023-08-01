using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(HunterMovement))]
public class FollowPath : MonoBehaviour
{

    private static string PATH_PARENT_NAME = "Path";
    private static float DISTANCE_THRESHOLD = 0.5f;

    private HunterMovement _movement;
    public PathDefiner Path;

    private bool _waiting;

    void Start(){
        _movement = GetComponent<HunterMovement>();
        Path.Initialize();
        if (Path.transform.childCount == 0){
            enabled = false;
            return;
        }
        Invoke(nameof(GoToTarget), 0.1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_waiting || Vector3.Distance(transform.position, Path.Target.transform.position) > DISTANCE_THRESHOLD)
            return;

        if (Path.Target.waitingTime > 0f){
            _waiting = true;
            _movement.Stop();
            Invoke(nameof(StopWaiting), Path.Target.waitingTime);
        }
        Path.NextTarget();
        GoToTarget();

    }

    public void GoToTarget()
    {
        _movement.WalkToPos(Path.Target.transform.position);
    }

    private void StopWaiting(){
        _waiting = false;
        GoToTarget();
    }

    public void SetPath(PathDefiner path){
        Path = path;
    }

    private void OnDrawGizmos(){
        if(Path == null || Path.Target == null) return;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(Path.Target.transform.position, DISTANCE_THRESHOLD / 2f);
    }
    
    
}
