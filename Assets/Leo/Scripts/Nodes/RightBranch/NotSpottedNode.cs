using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSpottedNode : Node
{
    public NotSpottedNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.playerSpotted == false)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            MonsterAI.chasingPlayer = true;
            return NodeState.FAILURE;
        }
    }
}
