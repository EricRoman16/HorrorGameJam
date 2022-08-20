using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLastSeenDoorNode : Node
{
    private Transform currentPos;

    public TargetLastSeenDoorNode(Transform currentPos)
    {
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        Debug.Log("target last seen door");
        if (DoorScript.lastSeenDoor != null)
        {
            MonsterAI.currentTarget = DoorScript.lastSeenDoor;

            float dist = Vector3.Distance(currentPos.position, DoorScript.lastSeenDoor.transform.position);

            if (dist > 3)
            {
                return NodeState.RUNNING;
            }
            else
            {
                return NodeState.SUCCESS;
            }
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
