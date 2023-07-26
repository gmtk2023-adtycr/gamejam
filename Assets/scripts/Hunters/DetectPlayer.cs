using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class DetectPlayer : MonoBehaviour
{
    private void Start()
    {
        GetComponent<LightConeCollider>().OnDetectPlayer += OnDetectPlayer;
    }

    private void OnDetectPlayer(GameObject player)
    {
        Debug.Log("Detected by " + transform.parent.parent.gameObject.name);
        player.GetComponent<Movement>().Die();
    }
    

}