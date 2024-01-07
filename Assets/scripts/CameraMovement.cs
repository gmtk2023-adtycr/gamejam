using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smoothing;
    private Camera _camera;
    private Transform _target;

    private void Start(){
        _camera = GetComponent<Camera>();
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (transform.position == _target.position) return;
        
        Vector3 targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
        var grid = Grid.GetGridForPos(targetPosition);
        if(grid == null)
            return;
        var minPos = grid.WalkableLowerLeftCorner;
        var offset = new Vector3(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize) 
                     + Vector3.left + Vector3.down;
        minPos += offset;
        var maxPos = grid.WalkableUpperRightCorner;
        maxPos -= offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, .1f);

    }
    
}
