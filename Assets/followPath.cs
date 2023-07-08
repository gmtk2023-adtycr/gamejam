using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPath : MonoBehaviour
{
    public pathPoint point;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(point != null)
        {

            Vector3 direction = Vector3.Normalize( point.transform.position - transform.position);
            transform.position = transform.position + direction * Time.deltaTime* speed;

            if(Vector3.Distance(transform.position , point.transform.position) < 0.1f)
            {
                point = point.nextPoint;
            }

        }
    }
}
