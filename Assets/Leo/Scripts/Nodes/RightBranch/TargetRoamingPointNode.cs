using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRoamingPointNode : Node
{
    private Transform currentPos;

    public TargetRoamingPointNode( Transform currentPos)
    {
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        MonsterAI.currentTarget = MonsterAI.currentTargetRoaming;

        float dist = Vector3.Distance(currentPos.position, MonsterAI.currentTargetRoaming.transform.position);

        if (dist > 3)
        {
            return NodeState.RUNNING;
        }
        else
        {
            MonsterAI.currentTargetRoom = null;
            MonsterAI.currentTargetDoor = null;
            MonsterAI.currentTargetRoaming = null;

            return NodeState.SUCCESS;
        }
    }
}
