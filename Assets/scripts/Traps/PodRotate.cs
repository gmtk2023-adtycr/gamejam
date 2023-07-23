using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodRotate : MonoBehaviour
{
    
    public float pivotSpeed = 2f; // rotation/second


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, 0f, 36 * pivotSpeed * Time.deltaTime, Space.Self);
    }

}
