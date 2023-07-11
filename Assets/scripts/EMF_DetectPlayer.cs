using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMF_DetectPlayer : MonoBehaviour
{

    public GameObject noise;
    public AudioSource m_audio;

    private void Start()
    {
        m_audio = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(noise);
            m_audio.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_audio.Stop();
        }
    }
}
