using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRoomNode : Node
{
    public TargetRoomNode() { }

    public override NodeState Evaluate()
    {
        MonsterAI.chasingPlayer = false;
        FindRandomRoom();
        return NodeState.SUCCESS;
    }

    private void FindRandomRoom()
    {
        if (MonsterAI.currentTargetRoom == null)
        {
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
