using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPath : MonoBehaviour
{
    public pathPoint point;
    public float speed;
    

    private Rigidbody2D rb;
    public bool waiting = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting || point == null)
            return;
        
        if (Vector3.Distance(transform.position, point.transform.position) < 0.3f){
            if (point.waitingTime > 0f){
                waiting = true;
                rb.velocity = Vector2.zero;
                Invoke(nameof(StopWaiting), point.waitingTime);
            }
            point = point.nextPoint;
        }
        else
            GoToNextPoint();

    }

    private void GoToNextPoint()
    {
        Vector3 direction = Vector3.Normalize(point.transform.position - transform.position);
        rb.velocity = direction * speed;
    }

    private void StopWaiting(){
        waiting = false;
        GoToNextPoint();
    }
}