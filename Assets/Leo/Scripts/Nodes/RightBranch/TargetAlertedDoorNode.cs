using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAlertedDoorNode : Node
{
    private float alertedSpeed;

    public TargetAlertedDoorNode(float alertedSpeed)
    {
        this.alertedSpeed = alertedSpeed;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Alerted");
        MonsterAI.currentSpeed = alertedSpeed;
        TargetAlertedDoor();
        return NodeState.SUCCESS;
    }

    private void TargetAlertedDoor()
    {
        MonsterAI.currentTargetRoaming = null;
        MonsterAI.currentTargetRoom = null;

        if (DoorCollision.alertedRoom != null)
        {
            MonsterAI.currentTargetDoor = DoorCollision.alertedRoom.GetComponent<RoomScript>().GetDoorInRoom();
        }
    }
}
