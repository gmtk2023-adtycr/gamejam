using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFlash : MonoBehaviour
{

    public float timeOn = .2f;
    public float timeOff = 2;
    public AudioSource audioOn;
    public AudioSource audioOff;
    
    
    private float time;
    private bool isOn;
    private GameObject LightCone;


    // Start is called before the first frame update
    void Start(){
        LightCone = transform.GetChild(0).gameObject;
        isOn = LightCone.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(isOn && time>timeOn) 
        {
            time = 0;
            LightCone.SetActive(false);
            isOn = false;
        }
        else if(!isOn && time>timeOff) 
        {
            time = 0;
            LightCone.SetActive(true);
            isOn = true;
        }
        if(isOn && !audioOn.isPlaying) 
        {
            audioOn.Play();
            audioOff.Stop();
        }
        else
            if(!isOn && !audioOff.isPlaying)
        {
            audioOn.Stop();
            audioOff.Play();
        }
    }
}
