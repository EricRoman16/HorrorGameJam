using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLastSeenDoorNode : Node
{
    public TargetLastSeenDoorNode() { }

    public override NodeState Evaluate()
    {
        if (DoorScript.lastSeenDoor != null)
        {
            MonsterAI.currentTarget = DoorScript.lastSeenDoor;
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
