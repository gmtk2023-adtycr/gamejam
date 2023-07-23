using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{

    private Light2D light2D;
    
    // Start is called before the first frame update
    void Start(){
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update(){
        //light2D.intensity = 0.40f + Mathf.Sin(Time.time * 2f) * 0.15f;
    }
}
