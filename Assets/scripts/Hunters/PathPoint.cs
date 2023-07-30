using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PathPoint : MonoBehaviour
{


    public float waitingTime = 0f; // seconds
    public PathPoint NextPoint => transform.parent.GetChild(NextIndex).GetComponent<PathPoint>();

    private int Index => transform.GetSiblingIndex();
    private int NextIndex => (Index+1) % transform.parent.childCount;
    
 }