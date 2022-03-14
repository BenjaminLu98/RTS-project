using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PathFinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    //private const int MOVE_DIAGONAL_COST = 14;

    private List<GridData> openList;
    private List<GridData> closeList;

    /// <summary>
    /// It uses DFS search to find the minimum FCost pathfrom (startx, startz) to (endX,endZ).
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="startZ"></param>
    /// <param name="endX"></param>
    /// <param name="endZ"></param>
    /// <returns></returns>
    public List<GridData> FindPath(int startX, int startZ, int endX, int endZ)
    {
        GridData startNode = GridSystem.current.getGridData(startX, startZ);
        GridData endNode = GridSystem.current.getGridData(endX, endZ);
        openList = new List<GridData> { startNode };
        closeList = new List<GridData>();
        
        for(int x=0; x < GridSystem.current.width; x++)
        {
            for(int z=0; z< GridSystem.current.height; z++)
            {
                var node = GridSystem.current.getGridData(x, z);
                node.GCost = int.MaxValue;
                node.calculateFCost();
                node.comeFromNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.calculateFCost();

        // In each iteration, the current node will update each non-close node if the FCost to reach that neighbour node
        // is smaller than the original FCost and add the selected noubour node into the openlist.
        while (openList.Count > 0)
        {
            GridData currentNode = getLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closeList.Add(currentNode);
            var currNeibourList = GridUtils.GetEmptyNeighbourList(currentNode);
            foreach(var neibour in currNeibourList)
            {
                if (closeList.Contains(neibour)) continue;
                int tentativeGCost = currentNode.GCost + CalculateDistanceCost(currentNode, neibour);
                if (tentativeGCost < neibour.GCost)
                {
                    neibour.GCost = tentativeGCost;
                    neibour.HCost = CalculateDistanceCost(neibour, endNode);
                    neibour.comeFromNode = currentNode;
                    neibour.calculateFCost();

                    if (!openList.Contains(neibour))
                    {
                        openList.Add(neibour);
                    }
                }
            }
           
        }

        Debug.LogError(System.Reflection.MethodBase.GetCurrentMethod().Name + $": start node may be invalid!{startNode.Position}");
        return null;
    }
    public List<GridData> CalculatePath(GridData endNode)
    {
        List<GridData> path = new List<GridData>();
        path.Add(endNode);
        var curr = endNode;
        while (curr.comeFromNode != null)
        {
            path.Add(curr.comeFromNode);
            curr = curr.comeFromNode;
        }
        return path;
    }

    private int CalculateDistanceCost(GridData a, GridData b)
    {
        int xDistance = Mathf.Abs(a.Position.x - b.Position.x);
        int yDistance = Mathf.Abs(a.Position.y - b.Position.y);
        //int remaining = Mathf.Abs(xDistance - yDistance);

        //return remaining * MOVE_STRAIGHT_COST + Mathf.Min(xDistance, yDistance) * MOVE_DIAGONAL_COST;
        return (xDistance + yDistance) * MOVE_STRAIGHT_COST;
    }

    private GridData getLowestFCostNode(List<GridData> nodeList)
    {
        GridData lowestFCostNode = nodeList[0];
        for(int i = 1; i < nodeList.Count; i++)
        {
            if (nodeList[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = nodeList[i];
            }
        }
        return lowestFCostNode;
    }
}

