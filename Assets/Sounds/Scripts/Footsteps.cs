using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // [Header("Footsteps")]
    // public List<AudioClip> Default_footsteps;
    // public List<AudioClip> Wood_footsteps;
    // public List<AudioClip> Water_footsteps;
    
    // enum FSMaterial{Wood,Water,Empty}

    // private AudioSource foostepSource;

    // void Start()
    // {
    //     footstepSource = GetComponent<AudioSource>();
    // }

    [SerializeField] private AudioClip[] myaudioclips;
    [SerializeField] private AudioSource audioSource;
        [Range(0.0f,3.0f)] public float volume = 1.0f;


    private void Step(){
        AudioClip myaudioclips = GetRandomClip();
        audioSource.PlayOneShot(myaudioclips);
    }
    private AudioClip GetRandomClip(){
        return myaudioclips[UnityEngine.Random.Range(0, myaudioclips.Length)];
        audioSource.volume = Random.Range(0.02f, 0.05f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
    }



    // void PlayFoostep(){
    //     AudioClip clip = null;
    //     FSMaterial surface = SurfaceSelect();

    //     switch(surface){
    //         case FSMaterial.Wood:
    //             clip = Wood_footsteps[Random.Range(0, Wood_footsteps.Count)];
    //             break;
    //         case FSMaterial.Water:
    //             clip = Water_footsteps[Random.Range(0, Water_footsteps.Count)];
    //             break;
    //         default:
    //             break;}
    //     Debug.Log(surface);

    //     if (surface != FSMaterial.Empty)
    //     {
    //         footstepSource.clip = clip;
    //         footstepSource.volume = Random.Range(0.01f, 0.03f);
    //         footstepSource.pitch = Random.Range(0.5f, 1f);
    //         footstepSource.Play();
    //     }
    // }
}