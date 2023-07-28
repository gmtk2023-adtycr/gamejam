using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterMovement : MonoBehaviour
{

    public float Speed;
    
    private Vector3 _target;
    private bool _moving;
    
    
    private Rigidbody2D _body;

    private void Awake(){
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update(){
        if(!_moving) return;
        
        Vector3 direction = Vector3.Normalize(_target - transform.position);
        _body.velocity = direction * Speed;
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
