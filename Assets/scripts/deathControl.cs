using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathControl : MonoBehaviour
{
    public static bool isdetect = false;

    public GameObject deathSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deathSound.SetActive(isdetect);
    }
}
