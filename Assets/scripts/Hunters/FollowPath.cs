using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_path.Over){
            var pop = true;
            if (_path is PathDefiner p){
                p.gameObject.SetActive(false);
                if (p.NextPathWhenOver){
                    var i = p.transform.GetSiblingIndex();
                    var nextPath = p.transform.parent.GetChild(i + 1);
                    _paths.Pop();
                    SetPath(nextPath.GetComponent<IPath>());
                    pop = false;
                }
            }
            if(pop)
                _paths.Pop();
            if(_path is PathDefiner np)
                np.gameObject.SetActive(true);
        }
        if (_waiting || _paths.Count == 0 || _path.Target == null) return;
        
        if(transform.parent.name == "Hunter1WithPath")
            print("Following " + _path.Target.Position);
        
        if (Vector2.Distance(transform.position, _path.Target.Position) < DISTANCE_THRESHOLD)
        {
            _path.Target.Arrival();
            if (_path.Target.WaitingTime > 0f)
            {
                _waiting = true;
                Stop();
                StartCoroutine(StopWaiting(_path.Target));
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

    public void NextTarget(){
        _path.NextTarget();
    }

    private IEnumerator StopWaiting(IPointPath point){
        yield return new WaitForSeconds(_path.Target.WaitingTime);
        point.Departure();
        _waiting = false;
        GoToTarget();
    }

    public void SetPath(IPath newPath){
        _paths.Push(newPath);
        _path.Initialize();
    }

    public void SetPathAndTP(GameObject newPath){
        if(_path is PathDefiner p)
            p.gameObject.SetActive(false);
        SetPath(newPath.GetComponent<IPath>());
        transform.position = _path.Target.Position;
        newPath.SetActive(true);
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