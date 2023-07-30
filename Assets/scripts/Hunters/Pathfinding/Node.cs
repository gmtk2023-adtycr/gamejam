using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public readonly Vector3Int Position;
    public readonly bool Walkable;
    public readonly int GridX;
    public readonly int GridY;
    public Node Parent;

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public Node(Vector3Int pos, bool walkable, int gridX, int gridY){
        Position = pos;
        Walkable = walkable;
        GridX = gridX;
        GridY = gridY;
    }
}
