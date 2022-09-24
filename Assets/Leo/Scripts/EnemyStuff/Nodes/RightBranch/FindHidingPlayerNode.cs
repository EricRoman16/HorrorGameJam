using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHidingPlayerNode : Node
{
    public FindHidingPlayerNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.targetHidingSpot == Player.currentHidingSpot)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
