using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrapDetectPlayer : MonoBehaviour
{
    
    public float Delay = 1;
    
    private GameObject _noisePrefab;
    private bool _active = true;

    private void Awake()
    {
        _noisePrefab = Resources.Load<GameObject>("Noise");
        GetComponent<LightConeCollider>().OnDetectPlayer += OnDetectPlayer;
    }

    private void OnDetectPlayer(GameObject player)
    {
        if(!_active) return;
        _active = false;
        Debug.Log($"Detected by {gameObject.transform.parent.parent.name}");
        Instantiate(_noisePrefab).transform.position = player.transform.position;
        Invoke(nameof(Activate), Delay);
    }

    private void Activate(){
        _active = true;
    }

}