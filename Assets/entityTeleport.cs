using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class entityTeleport : MonoBehaviour
{
    public GameObject recever;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = recever.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, recever.transform.position, Color.red);
    }
}