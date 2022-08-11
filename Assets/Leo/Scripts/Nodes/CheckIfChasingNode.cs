using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfChasingNode : Node
{
    public CheckIfChasingNode() { }

    public override NodeState Evaluate()
    {
        return MonsterAI.chasingPlayer ? NodeState.FAILURE : NodeState.SUCCESS;
    }
}
