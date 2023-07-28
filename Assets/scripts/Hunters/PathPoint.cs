using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class PathPoint : MonoBehaviour
{

    private static Vector3 DrawOffset = (Vector3.up + Vector3.right) / 2;

    public float waitingTime = 0f; // seconds
    public PathPoint NextPoint => transform.parent.GetChild(NextIndex).GetComponent<PathPoint>();

    private int Index => transform.GetSiblingIndex();
    private int NextIndex => (Index+1) % transform.parent.childCount;


    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + DrawOffset, NextPoint.transform.position + DrawOffset, Color.yellow);
    }

    private void OnDrawGizmosSelected(){
        Handles.Label(transform.position, $"p_{Index+1}");
    }
}