using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[ExecuteAlways]
public class Teleporter : MonoBehaviour
{
    public GameObject receiver;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.position = receiver.transform.position;
        FollowPath fpath = other.GetComponent<FollowPath>();
        if(fpath != null && Vector3.Distance( fpath.point.transform.position,transform.position)<2f)
        {
            fpath.point = fpath.point.nextPoint;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, receiver.transform.position, Color.red);
    }
}