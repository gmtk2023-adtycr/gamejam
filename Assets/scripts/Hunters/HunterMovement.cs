using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HunterMovement : MonoBehaviour
{

    public float Speed;
    public Tilemap Floor;

    private Rigidbody2D _body;
    private PathFinding _pathFinding;
    
    private Vector3 _target;
    private bool _moving;
    
    private List<Node> _path;
    private int _pathIndex;
    
    

    private void Start(){
        _body = GetComponent<Rigidbody2D>();
        _pathFinding = new PathFinding(Floor);
    }

    private void FixedUpdate(){
        if(!_moving) return;
        if(_path.Count == 0) return;

        if (Vector3.Distance(transform.position, _path[_pathIndex].Position) < .2f){
            if (_pathIndex == _path.Count - 1){
                Stop();
                return;
            }
            _pathIndex++;
        }


        Vector3 direction = Vector3.Normalize(_path[_pathIndex].Position - transform.position);
        _body.velocity = Vector2.Lerp(_body.velocity, direction * Speed, 0.1f);
    }

    public void WalkToPos(Vector3 pos){
        if(_target.Equals(pos))
            return;
        _target = pos;
        _path = _pathFinding.FindPath(transform.position, _target);
        _pathIndex = 0;
        _moving = true;
    }

    public void Stop(){
        _moving = false;
        _body.velocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected(){
        if(_path == null) return;
        for (int i = 1; i < _path.Count; i++){
            Gizmos.DrawLine(_path[i-1].Position, _path[i].Position);
        }
    }
}