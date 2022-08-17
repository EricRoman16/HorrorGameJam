using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedNode : Node
{
    public SpottedNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.playerSpotted == false)
        {
            return NodeState.FAILURE;
        }
        else
        {
            MonsterAI.chasingPlayer = true;
            return NodeState.SUCCESS;
        }
    }
}
