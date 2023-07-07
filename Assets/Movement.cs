using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Rigidbody2D body;

    private float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        //FindFirstObjectByType(typeof(Camera)).GameObject().transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = Input.GetAxis("Horizontal");
        float dy = Input.GetAxis("Vertical");

        float diagonal = 1f;
        if (dx != 0 && dy != 0)
            diagonal = 0.707f; // 1 / sqrt(2)

        body.velocity = new Vector3(dx, dy) * speed * diagonal;
    }
}
