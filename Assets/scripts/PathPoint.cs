using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class PathPoint : MonoBehaviour
{
    public PathPoint nextPoint;
    public float waitingTime = 0f; // seconds

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(nextPoint != null) 
            Debug.DrawLine(transform.position, nextPoint.transform.position, Color.yellow);
    }
}
