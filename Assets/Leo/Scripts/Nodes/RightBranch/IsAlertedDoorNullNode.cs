using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class IsAlertedDoorNullNode : Node
{
    public IsAlertedDoorNullNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.alerted == true && MonsterAI.chasingPlayer == false)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
