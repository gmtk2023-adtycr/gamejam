using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UsableItem : MonoBehaviour
{

    public bool Active;
    public bool DestroyOnDone = true;
    public event Action OnUse;


    private bool _playerHere;
    private Material _outlineMaterial;
    private static readonly int Active1 = Shader.PropertyToID("_Active");


    [SerializeField] private AudioSource Task_AudioSource;
    [SerializeField] private AudioClip Interaction_Sound;
    [Range(0.0f,3.0f)] public float volume = 1.0f;

    private void Start(){
        _outlineMaterial = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerHere && Active)
        {
            OnUse?.Invoke();
            Task_AudioSource.PlayOneShot(Interaction_Sound, volume);
            if(DestroyOnDone)
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
