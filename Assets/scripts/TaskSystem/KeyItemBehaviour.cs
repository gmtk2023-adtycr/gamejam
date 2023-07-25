using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyItemBehaviour : MonoBehaviour
{

    public event Action OnGet;
    public bool DestroyOnDone;

    private bool _playerHere;
    public bool used;
    private GameObject _interactIndicator;

    private void Awake(){
        _interactIndicator = transform.GetChild(0).gameObject;
        _interactIndicator.transform.localScale =
            new Vector3(1f / transform.localScale.x, 1f / transform.localScale.y, 1f);
        _interactIndicator.SetActive(false);
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && _playerHere && !used)
        {
            used = true;
            OnGet?.Invoke();
            if(DestroyOnDone)
                Destroy(gameObject);
        }
    }

    public void Reset(){
      used = false;
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(used || !collision.gameObject.CompareTag("Player")) return;

        _playerHere = true;
        _interactIndicator.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;

        _playerHere = false;
        _interactIndicator.SetActive(false);
    }

}
