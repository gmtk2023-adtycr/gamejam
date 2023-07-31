using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(UsableItem))]
public class KeyItemBehaviour : MonoBehaviour
{

    public bool DestroyOnDone;

    public event Action OnGet;
    private UsableItem _usableItem;


    private void Start(){
        _usableItem = GetComponent<UsableItem>();
        _usableItem.OnUse += OnItemUsed;
    }

    private void OnItemUsed(){
        OnGet?.Invoke();
        if(DestroyOnDone)
            Destroy(gameObject);
    }


}
