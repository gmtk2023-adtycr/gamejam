using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF_DetectPlayer : MonoBehaviour
{

    public GameObject noise;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            Instantiate(noise);
    }
}
