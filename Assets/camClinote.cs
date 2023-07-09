using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camClinote : MonoBehaviour
{
    public GameObject obj;

    public float timeOn = 1;
    public float timeOff = 1;

    float time;

    bool isOn;

    public AudioSource audioOn;
    public AudioSource audioOff;

    // Start is called before the first frame update
    void Start()
    {
        isOn = obj.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(isOn && time>timeOn) 
        {
            time = 0;
            obj.SetActive(false);
            isOn = false;
        }
        else if(!isOn && time>timeOff) 
        {
            time = 0;
            obj.SetActive(true);
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
