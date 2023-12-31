using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{

    public static Action enterZone;
    public static Action exitZone;

    private GameObject interactText;
    
    private void Awake(){
        interactText = transform.GetChild(0).gameObject;
    }

    private void OnEnable()
    {
        enterZone += enableUI;
        exitZone += disableUI;
    }
    private void OnDisable()
    {
        enterZone -= enableUI;
        exitZone -= disableUI;
    }
    void enableUI()
    {
        interactText.SetActive(true);
    }
    void disableUI()
    {
        interactText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        disableUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
