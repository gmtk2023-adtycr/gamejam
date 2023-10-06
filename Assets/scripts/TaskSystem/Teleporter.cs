using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum TeleporterType
{
    OnUse,
    OnEnter
}

public class Teleporter : MonoBehaviour
{

    public Teleporter Destination;
    public TeleporterType Type;
    
    private UsableItem _usableItem;
    private Dictionary<GameObject, bool> _dontTP = new();

    private void Start(){
        if (Type == TeleporterType.OnUse){
            _usableItem = GetComponent<UsableItem>();
            _usableItem.OnUse += TeleportPlayer;
        }

    }

    private void TeleportPlayer(){
        TeleportEntity(GameObject.FindGameObjectWithTag("Player"));
    }

    private void TeleportEntity(GameObject entity){
        var posDiff = entity.transform.position - transform.position;
        Destination.ReceiveEntity(entity, posDiff);
    }

    private void ReceiveEntity(GameObject entity, Vector3 posDiff){
        if(_dontTP.ContainsKey(entity)) return;
        _dontTP.Add(entity, true);
        entity.transform.position = transform.position + posDiff;
        entity.GetComponent<FollowPath>()?.NextTarget();
    }

    private void OnTriggerEnter2D(Collider2D col){
        if (Type == TeleporterType.OnEnter && !_dontTP.ContainsKey(col.gameObject)){
            TeleportEntity(col.gameObject);
        }    
    }

    private void OnTriggerExit2D(Collider2D other){
        _dontTP.Remove(other.gameObject);
    }

    private void OnDrawGizmosSelected(){
        if (Destination != null)
            Debug.DrawLine(transform.position, Destination.transform.position, Color.yellow);
    }
}