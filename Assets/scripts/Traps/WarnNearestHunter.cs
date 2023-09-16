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

        var path = MakeNoisePath(nearestHunter);
        if(path != null)
            nearestHunter.GetComponent<FollowPath>().SetPath(path);
        Destroy(this);
    }

    private IPath MakeNoisePath(GameObject hunter)
    {
        var nodePath = PathFinding.FindPath(Grid.GetGridForPos(transform.position), hunter.transform.position, transform.position);
        return nodePath == null || nodePath.Count == 0 ? null : new NodePath(nodePath);
    }
}
