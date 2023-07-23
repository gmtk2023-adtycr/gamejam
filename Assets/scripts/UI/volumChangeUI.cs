using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumChangeUI : MonoBehaviour
{
    public Slider slider;
    public AudioMixer mixer;
    public string strVal;

    // Start is called before the first frame update
    void Start()
    {
        float val;
        mixer.GetFloat(strVal, out val);
        slider.value = val;
    }

    public void valChange(float val)
    {
        mixer.SetFloat(strVal, val);
    }

    // Update is called once per frame
    void Update()
    {
        //masterMixer.SetFloat("MusicVol", soundLevel);
    }
}
