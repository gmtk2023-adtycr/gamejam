using UnityEngine;

public interface IPointPath
{
    public float WaitingTime { get; }
    public Vector3 Position { get; }

    public void Arrival();
    public void Departure();
}