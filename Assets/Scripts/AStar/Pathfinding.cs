using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Pathfinding: MonoBehaviour {
    // ıı
    PathRequestManager requestManager;
    Grid grid;
    private void Awake()
    { 
        grid = GetComponent<Grid>();
    }

    
    public void  FindPath(PathRequest request,Action<PathResult> callback)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Vector3[] wayPoints = new Vector3[0];
        bool pathSuccess = false;
        Node startNode = grid.NodeFromWorldPoint(request.pathStart);
        Node targetNode = grid.NodeFromWorldPoint(request.pathEnd);
        startNode.parent = startNode;
        if(startNode.walkable && targetNode.walkable)
        {



            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
            while(openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();

                closedSet.Add(currentNode);
                if(currentNode == targetNode)
                {
                    sw.Stop();
                    //print("path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    break;
                }
                foreach(Node neighbor in grid.GetNeighbors(currentNode))
                {
                    if(!neighbor.walkable || closedSet.Contains(neighbor))
                    {
                        continue;
                    }
                    int newMovementCostToNeighbor = currentNode.gCost + GetDistanceNode(currentNode, neighbor) + neighbor.movementPenalty;
                    if(newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistanceNode(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if(!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        } else
                        {
                            openSet.UpdateItem(neighbor);
                        }
                    }
                }

            }
        } 
        if(pathSuccess)
        {
            wayPoints = RetracePath(startNode, targetNode);
            pathSuccess = wayPoints.Length > 0;

        }
        callback(new PathResult(wayPoints, pathSuccess, request.callback));
    }
    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
       Vector3[] wayPoints = SimplifyPath(path);
   Array.Reverse(wayPoints) ;
        return wayPoints;

    }
    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> wayPoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                wayPoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return wayPoints.ToArray();
    }
    int GetDistanceNode(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if(dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);

        }
        return 14 * dstX + 10 * (dstY - dstX);
    }

}
