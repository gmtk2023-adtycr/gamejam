using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowPath : MonoBehaviour
{

    private static float DISTANCE_THRESHOLD = 0.5f;
    
    public float Speed = 2;
    public PathDefiner StartingPath;
    
    private Rigidbody2D _body;

    private Stack<IPath> _paths = new();
    private IPath _path => _paths.Count == 0 ? null : _paths.Peek();
    private bool _waiting;

    void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        if (StartingPath != null)
        {
            SetPath(StartingPath);
        }
    }

    private void FixedUpdate()
    {
        if (_waiting || _paths.Count == 0) return;

        if (_path.Over)
            _paths.Pop();
        if(_path.Target == null)
            Debug.LogError($"{name} as null target {_path}");
        
        if (Vector2.Distance(transform.position, _path.Target.Position) < DISTANCE_THRESHOLD)
        {
            if (_path.Target.WaitingTime > 0f)
            {
                _waiting = true;
                Stop();
                Invoke(nameof(StopWaiting), _path.Target.WaitingTime);
            }
            _path.NextTarget();
        }
        
        if(!_waiting)
            GoToTarget();
    }

    public void GoToTarget()
    {
        var direction = _path.Target.Position - transform.position;
        _body.velocity = direction.normalized * Speed;
    }

    public void Stop()
    {
        _body.velocity = Vector2.zero;
    }

    private void StopWaiting(){
        _waiting = false;
        GoToTarget();
    }

    public void SetPath(IPath newPath){
        _paths.Push(newPath);
        _path.Initialize();
    }

    public void RemovePath()
    {
        _paths.Pop();
    }

    private void OnDrawGizmos(){
        if(_path?.Target == null) return;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(_path.Target.Position, DISTANCE_THRESHOLD / 2f);
    }
    
    
}
