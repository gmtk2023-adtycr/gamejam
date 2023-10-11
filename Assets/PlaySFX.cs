using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{
public AudioClip sfx_transitionIN;
public AudioClip sfx_transitionOUT;
public AudioSource AudioSource;
    // Update is called once per frame
    void PlaySFX_TransitionIN()
    {
        AudioSource.PlayOneShot(sfx_transitionIN);
    }


        void PlaySFX_TransitionOUT()
    {
        AudioSource.PlayOneShot(sfx_transitionOUT);
    }
}
