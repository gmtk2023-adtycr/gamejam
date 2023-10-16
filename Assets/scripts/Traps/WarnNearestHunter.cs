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
        Grid grid = Grid.GetGridForPos(transform.position);

        // Check if the grid is null before proceeding
        if (grid == null)
        {
            Debug.LogWarning("Grid is null! (script: WarnNearestHunter)");
            return null;
        }

        var nodePath = PathFinding.FindPath(grid, hunter.transform.position, transform.position);

        // Check if the nodePath is valid before creating a new NodePath
        if (nodePath == null || nodePath.Count == 0)
        {
            return null;
        }

        return new NodePath(nodePath);
    }
}