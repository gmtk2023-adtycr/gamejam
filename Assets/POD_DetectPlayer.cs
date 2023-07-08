using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POD_DetectPlayer : MonoBehaviour
{

    public GameObject noise;

    public float pivotSpeed = .2f; // rotation/second


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0f, 0f, 36 * pivotSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            Instantiate(noise);
    }
}
