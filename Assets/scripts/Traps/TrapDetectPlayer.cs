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
        m_audio = GetComponent<AudioSource>();
        GetComponent<LightConeCollider>().OnDetectPlayer += OnDetectPlayer;
    }

    private void OnDetectPlayer(GameObject player)
    {
        Debug.Log($"Detected by {gameObject.transform.parent.parent.name}");
        Instantiate(noise);
        m_audio.Play();
    }

}