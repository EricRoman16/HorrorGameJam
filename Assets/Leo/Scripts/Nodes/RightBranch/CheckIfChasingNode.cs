using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfChasingNode : Node
{
    public CheckIfChasingNode() { }

    public override NodeState Evaluate()
    {
        //Debug.Log("Check if chasing " + MonsterAI.chasingPlayer);
        return MonsterAI.chasingPlayer ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
