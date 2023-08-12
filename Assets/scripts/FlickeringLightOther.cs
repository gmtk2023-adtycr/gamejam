using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLightOther : MonoBehaviour
{

    private Light2D light2D;
    
    // Start is called before the first frame update
    void Start(){
        light2D = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update(){
        light2D.intensity = 0.30f + Mathf.Sin(Time.time * .25f) * 0.1f;
    }
}
