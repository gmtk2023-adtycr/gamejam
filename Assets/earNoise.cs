using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class earNoise : MonoBehaviour
{
    RoomPass room;

    public GameObject earsObject;
    public float speed;

    Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("room"))
        {
            room = other.GetComponent<RoomPass>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Contains("room"))
        {
            RoomPass temp = other.GetComponent<RoomPass>();
            if (room == temp)
                room = null;
        }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(room != null)
        {
            Vector3 point = room.findDirectionTo(earsObject.transform.position);
            Debug.DrawLine(transform.position, point,Color.red);

            Vector3 dir = Vector3.Normalize(point - transform.position);

            rb.velocity = dir * speed;
            //transform.position = transform.position + dir * Time.deltaTime * speed;
        }

    }

}
