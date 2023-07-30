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
    
    

    private void Start(){
        _body = GetComponent<Rigidbody2D>();
        _pathFinding = new PathFinding(Floor);
    }

    private void FixedUpdate(){
        if(!_moving) return;
        var path = _pathFinding.FindPath(transform.position, _target);
        if(path.Count == 0) return;


        Vector3 direction = Vector3.Normalize(path[0] - transform.position);
        _body.velocity = Vector2.Lerp(_body.velocity, direction * Speed, 0.1f);
    }

    public void WalkToPos(Vector3 pos){
        _target = pos;
        _moving = true;
    }

    public void Stop(){
        _moving = false;
        _body.velocity = Vector2.zero;
    }
    
}
