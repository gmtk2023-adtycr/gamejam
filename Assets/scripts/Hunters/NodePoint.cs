using UnityEngine;

public class NodePoint : IPointPath
{
    
    public float WaitingTime { get; set; }
    public Vector3 Position { get; }

    public NodePoint(Node node)
    {
        Position = node.Position;
    }
    
    public void Arrival(){
    }

    public void Departure(){
    }
    
}