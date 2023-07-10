using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF_DetectPlayer : MonoBehaviour
{

    public GameObject noise;
    public AudioSource audio;

    private void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(noise);
            audio.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audio.Stop();
        }
    }
}
