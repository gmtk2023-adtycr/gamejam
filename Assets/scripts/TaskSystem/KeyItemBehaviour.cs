using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyItemBehaviour : MonoBehaviour
{

    public Subject<bool> DoneSubject = new(false);
    public bool DestroyOnDone;
    
    private bool _playerHere;
    private GameObject _interactIndicator;

    private void Start(){
        _interactIndicator = transform.GetChild(0).gameObject;
        _interactIndicator.SetActive(false);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && _playerHere){
            DoneSubject.Next(true);
            if(DestroyOnDone)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;
        
        _playerHere = true;
        _interactIndicator.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;
        
        _playerHere = false;
        _interactIndicator.SetActive(false);
    }

}
