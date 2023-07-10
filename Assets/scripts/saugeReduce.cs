using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saugeReduce : MonoBehaviour
{
    public float speedReduce = 0.25f;
    public float sizeNoColider = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale -= transform.localScale * speedReduce * Time.deltaTime;

        if(transform.localScale.magnitude < sizeNoColider)
            this.GetComponent<CircleCollider2D>().enabled = false;

        if(transform.localScale.magnitude < 0.1)
            Destroy(gameObject);
    }
}
