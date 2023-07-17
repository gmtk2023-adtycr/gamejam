using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightConeCollider : MonoBehaviour
{

    [Min(1)]
    public int steps = 10;

    void FixedUpdate(){
        var start = transform.parent.position;
        foreach (var ray in GetVectors()){
            RaycastHit2D hit = Physics2D.Raycast(start, ray - start, Vector3.Distance(start, ray), LayerMask.GetMask("Player"));
            if (hit.collider != null){
                gameObject.SendMessage("OnTriggerEnter2D", hit.collider);
                break;
            }
        }
    }

    private IEnumerable<Vector3> GetVectors(){

        var _light2D = GetComponent<Light2D>();

        Vector3 start = transform.parent.position;
        float base_rotation = (transform.parent.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
        float dist = _light2D.pointLightOuterRadius;
        float diff_angle = _light2D.pointLightOuterAngle * Mathf.Deg2Rad / steps;
        
        for (int i = -steps/2; i < steps/2; i++){
            float x = start.x + dist * MathF.Cos(base_rotation + diff_angle * i);
            float y = start.y + dist * MathF.Sin(base_rotation + diff_angle * i);
            var end = new Vector3(x, y, start.z);
            RaycastHit2D hit = Physics2D.Raycast(start, end - start, dist, LayerMask.GetMask("Collision"));
            yield return (hit.collider == null ? end : hit.point);
        }
        

    }

    private void OnDrawGizmosSelected(){
        foreach (var v in GetVectors()){
            Debug.DrawLine(transform.parent.position, v, Color.red);
        }
    }
}
