using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class KeyItemBehaviour : MonoBehaviour
{

    public bool Active{ get; set; }
    public bool DestroyOnDone;

    public event Action OnGet;

    private bool _playerHere;
    private Material _outlineMaterial;
    private static readonly int Active1 = Shader.PropertyToID("_Active");

    private void Start(){
        _outlineMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerHere && Active)
        {
            Active = false;
            OnGet?.Invoke();
            if (DestroyOnDone)
                Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(!Active || !collision.gameObject.CompareTag("Player")) return;

        _playerHere = true;
        _outlineMaterial.SetInteger(Active1, 1);
    }

    private void OnTriggerExit2D(Collider2D collision){
        if(!collision.gameObject.CompareTag("Player")) return;

        _playerHere = false;        
        _outlineMaterial.SetInteger(Active1, 0);
    }

}
