using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRoomNode : Node
{
    private float roamSpeed;

    public TargetRoomNode(float roamSpeed)
    {
        this.roamSpeed = roamSpeed;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Target Room Node");
        MonsterAI.currentSpeed = roamSpeed;
        MonsterAI.chasingPlayer = false;
        FindRandomRoom();
        return NodeState.SUCCESS;
    }

    private void FindRandomRoom()
    {
        if (MonsterAI.currentTargetRoom == null)
        {
            //Debug.Log("setting room");
            MonsterAI.currentTargetRoom = RoomScript.rooms[Random.Range(0, RoomScript.rooms.Count)];
            while (MonsterAI.currentTargetRoom == MonsterAI.currentEnemyRoom)
            {
                MonsterAI.currentTargetRoom = RoomScript.rooms[Random.Range(0, RoomScript.rooms.Count)];
            }
            MonsterAI.currentTargetDoor = MonsterAI.currentTargetRoom.GetComponent<RoomScript>().GetDoorInRoom();
            MonsterAI.currentTargetRoaming = MonsterAI.currentTargetRoom.GetComponent<RoomScript>().roamingTarget;
        }
    }
}
