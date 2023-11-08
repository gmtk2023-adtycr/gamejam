using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    private Camera _camera;

    private void Start(){
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(transform.position != target.position){
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            var grid = Grid.GetGridForPos(targetPosition);
            var minPos = grid.WalkableLowerLeftCorner;
            var offset = new Vector3(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
            minPos += offset;
            var maxPos = grid.WalkableUpperRightCorner;
            maxPos -= offset;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPos.x, maxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPos.y, maxPos.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, .5f);
        }
    
    }
    
}
