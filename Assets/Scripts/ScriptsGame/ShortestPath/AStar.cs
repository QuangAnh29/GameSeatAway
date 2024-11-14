using UnityEngine;
using System.Collections.Generic;

public static class AStar
{
    public static List<AStarNode> FindPath(AStarNode[,] nodes, Vector2Int start, Vector2Int goal)
    {
        List<AStarNode> path = new List<AStarNode>();

        HashSet<AStarNode> openSet = new HashSet<AStarNode>();
        HashSet<AStarNode> closedSet = new HashSet<AStarNode>();

        AStarNode startNode = nodes[start.x, start.y];
        AStarNode goalNode = nodes[goal.x, goal.y];

        startNode.CostSoFar = 0;
        startNode.EstimatedTotalCost = HeuristicCostEstimate(startNode, goalNode);

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            AStarNode current = GetLowestCostNode(openSet);

            if (current == goalNode)
            {
                path = ReconstructPath(current);
                break;
            }

            openSet.Remove(current);
            closedSet.Add(current);

            foreach (AStarNode neighbor in GetNeighbors(current, nodes))
            {
                if (closedSet.Contains(neighbor))
                    continue;

                float newCost = current.CostSoFar + 1;

                if (newCost < neighbor.CostSoFar)
                {
                    neighbor.CameFrom = current;
                    neighbor.CostSoFar = newCost;
                    neighbor.EstimatedTotalCost = newCost + HeuristicCostEstimate(neighbor, goalNode);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return path;
    }

    private static float HeuristicCostEstimate(AStarNode node, AStarNode goal)
    {
        return Vector2Int.Distance(node.Position, goal.Position);
    }

    private static AStarNode GetLowestCostNode(HashSet<AStarNode> set)
    {
        AStarNode lowestCostNode = null;
        float lowestCost = float.MaxValue;

        foreach (AStarNode node in set)
        {
            if (node.EstimatedTotalCost < lowestCost)
            {
                lowestCost = node.EstimatedTotalCost;
                lowestCostNode = node;
            }
        }
        return lowestCostNode;
    }

    private static List<AStarNode> ReconstructPath(AStarNode current)
    {
        List<AStarNode> path = new List<AStarNode>();

        while (current != null)
        {
            path.Insert(0, current);
            current = current.CameFrom;
        }
        return path;
    }

    private static List<AStarNode> GetNeighbors(AStarNode node, AStarNode[,] nodes)
    {
        List<AStarNode> neighbors = new List<AStarNode>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0), // Left
            new Vector2Int(1, 0) // Right
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPosition = node.Position + direction;

            if (IsPositionValid(neighborPosition, nodes))
            {
                AStarNode neighborNode = nodes[neighborPosition.x, neighborPosition.y];
                HoldingBase neighborHoldingBase = neighborNode.HoldingBase;
                if (neighborHoldingBase.CanMove != false)
                {
                    neighbors.Add(neighborNode);
                }
            }
        }
        return neighbors;
    }

    private static bool IsPositionValid(Vector2Int position, AStarNode[,] nodes)
    {
        int maxX = nodes.GetLength(0);
        int maxY = nodes.GetLength(1);
        return position.x >= 0 && position.x < maxX &&
               position.y >= 0 && position.y < maxY;
    }
}
