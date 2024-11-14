using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarNode
{
    public Vector2Int Position { get; private set; }
    public HoldingBase HoldingBase { get; private set; }
    public float CostSoFar { get; set; }
    public float EstimatedTotalCost { get; set; }
    public AStarNode CameFrom { get; set; }

    public AStarNode(Vector2Int position, HoldingBase holdingBase)
    {
        Position = position;
        HoldingBase = holdingBase;
        CostSoFar = float.MaxValue;
        EstimatedTotalCost = float.MaxValue;
        CameFrom = null;
    }
}
