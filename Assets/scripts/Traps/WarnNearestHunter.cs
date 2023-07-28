using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarnNearestHunter : MonoBehaviour
{
    void Start(){
        var nearestHunter = GameObject.FindGameObjectsWithTag("Enemy")
            .ToList()
            .OrderBy(hunter => (transform.position - hunter.transform.position).magnitude)
            .First();
        nearestHunter.GetComponent<BehaviourManager>().TrackNoise(transform.position);
        Destroy(this);
    }

}
