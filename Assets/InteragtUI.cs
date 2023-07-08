using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteragtUI : MonoBehaviour
{
    public GameObject interagtText;


    public static Action enterZone;
    public static Action exitZone;

    private void OnEnable()
    {
        enterZone += activUI;
        exitZone += unactivUI;
    }
    private void OnDisable()
    {
        enterZone -= activUI;
        exitZone -= unactivUI;
    }
    void activUI()
    {
        interagtText.SetActive(true);
    }
    void unactivUI()
    {
        interagtText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        unactivUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
