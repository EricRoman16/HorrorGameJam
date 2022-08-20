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
        //Debug.Log("Target Roaming Points Node");

        MonsterAI.currentTarget = MonsterAI.currentTargetRoaming;

        float dist = Vector3.Distance(currentPos.position, MonsterAI.currentTargetRoaming.transform.position);

        if (dist > 1)
        {
            //Debug.Log("run");
            return NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("succ");
            return NodeState.SUCCESS;
        }
    }
}
