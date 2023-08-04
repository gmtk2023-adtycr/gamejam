using UnityEditor;
using UnityEngine;

public class PathPoint : MonoBehaviour, IPointPath
{
    public float waitingTime = 0f; // seconds
    public float WaitingTime => waitingTime;
    public Vector3 Position => transform.position;
}