using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding
{

    public static List<Node> FindPath(Grid grid, Vector3 startPos, Vector3 targetPos){
        if (grid.OutOfBound(startPos, targetPos)){
            //Debug.LogError("Out of bound !!");
            return new List<Node>();
        }
        
        var startNode = grid.NodeFromWorldPoint(startPos);
        var targetNode = grid.NodeFromWorldPoint(targetPos);

        var openSet = new Heap<Node>(grid.MaxSize);
        var closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0) {
            Node currentNode = openSet.RemoveFirst();
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
                return RetracePath(startNode,targetNode);
            

            foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + currentNode.Penalty;
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return new();
    }


    private static List<Node> RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }
        path.Reverse();

        return SimplifyPath(path);
    }
    
    private static List<Node> SimplifyPath(List<Node> path) {
        List<Node> waypoints = new();
        Vector2 directionOld = Vector2.zero;
		
        for (int i = 1; i < path.Count; i ++) {
            Vector2 directionNew = new Vector2(path[i-1].GridX - path[i].GridX,path[i-1].GridY - path[i].GridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i]);
            }
            directionOld = directionNew;
        }

        return waypoints;
    }

    private static int GetDistance(Node nodeA, Node nodeB) {
        int dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        if (dstX > dstY)
            return 14*dstY + 10* (dstX-dstY);
        return 14*dstX + 10 * (dstY-dstX);
    }
    
    
}