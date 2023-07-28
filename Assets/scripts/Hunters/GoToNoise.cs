using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum NoiseState
{
    None,
    Go,
    Stay,
    ComeBack
}

[RequireComponent(typeof(HunterMovement))]
public class GoToNoise : MonoBehaviour
{
    
    [SerializeField]
    private NoiseState _state = NoiseState.None;
    private HunterMovement _movement;    

    private Vector3 _noisePos;
    private Vector3 _startingPos;
    private bool _waiting;

    public event Action OnTrackOver;

    
    void Start()
    {
        _movement = GetComponent<HunterMovement>();
    }

    // Update is called once per frame
    void Update(){
        if (_state == NoiseState.Go)
            WalkToNoise();
        else if (_state == NoiseState.ComeBack)
            ComeBackToPath();
        else if (_state == NoiseState.Stay && !_waiting)
            Wait();
    }

    public void TrackNoise(Vector3 pos){
        _noisePos = pos;
        _state = NoiseState.Go;
        _startingPos = transform.position;
    }

    private void WalkToNoise(){
        if (Vector3.Distance(_noisePos, transform.position) < .5f)
            _state++;
        else
            _movement.WalkToPos(_noisePos);
    }

    private void ComeBackToPath(){
        if (Vector3.Distance(_startingPos, transform.position) < .5f){
            _state = NoiseState.None;
            OnTrackOver?.Invoke();
        }
        else
            _movement.WalkToPos(_startingPos);
    }

    private void Wait(){
        _waiting = true;
        _movement.Stop();
        Invoke(nameof(StopWaiting), 2f);
    }

    private void StopWaiting(){
        _waiting = false;
        _state = NoiseState.ComeBack;
    }
    
}

