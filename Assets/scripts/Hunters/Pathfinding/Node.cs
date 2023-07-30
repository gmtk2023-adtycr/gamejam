using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node>
{

    public readonly Vector3 Position;
    public readonly bool Walkable;
    public readonly int GridX;
    public readonly int GridY;
    public Node Parent;
    public int Penalty;
    public int HeapIndex{ get; set; }

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public Node(Vector3 pos, bool walkable, int gridX, int gridY){
        Position = pos;
        Walkable = walkable;
        GridX = gridX;
        GridY = gridY;
    }
    
    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

    public override string ToString(){
        return $"({GridX}, {GridY})";
    }
}
