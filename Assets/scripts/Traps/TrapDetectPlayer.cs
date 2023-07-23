using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrapDetectPlayer : MonoBehaviour
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
            Debug.Log($"Detected by {gameObject.transform.parent.parent.name}");
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
