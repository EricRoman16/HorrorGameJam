using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHidingSpot : Node
{
    private Transform currentPos;

    public TargetHidingSpot(Transform currentPos)
    {
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        MonsterAI.currentTarget = Player.currentHidingSpot;

        float dist = Vector3.Distance(currentPos.position, Player.currentHidingSpot.transform.position);

        if (dist > 3)
        {
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
