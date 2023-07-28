using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HunterMovement), typeof(FollowPath), typeof(GoToNoise))]
public class BehaviourManager : MonoBehaviour
{

    private FollowPath _followPath;
    private GoToNoise _goToNoise;
    
    void Start(){
        _followPath = GetComponent<FollowPath>();
        _goToNoise = GetComponent<GoToNoise>();
        _goToNoise.enabled = false;
        _goToNoise.OnTrackOver += ResumePath;
    }

    public void TrackNoise(Vector3 noisePose){
        _followPath.enabled = false;
        _goToNoise.enabled = true;
        _goToNoise.TrackNoise(noisePose);
    }

    public void ResumePath(){
        _followPath.enabled = true;
        _goToNoise.enabled = false;
    }
    
}
