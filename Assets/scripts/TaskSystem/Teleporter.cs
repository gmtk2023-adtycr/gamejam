using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public Teleporter Destination;
    
    private UsableItem _usableItem;

    private void Start(){
        _usableItem = GetComponent<UsableItem>();
        _usableItem.OnUse += TeleportPlayer;
    }

    private void TeleportPlayer(){
        GameObject.FindGameObjectWithTag("Player").transform.position = Destination.transform.position + Vector3.down * 2.2f;
    }

    private void OnDrawGizmosSelected(){
        if (Destination != null)
            Debug.DrawLine(transform.position, Destination.transform.position, Color.yellow);
    }
}
