﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class TargetRoamingPointNode : Node
{
    public TargetRoamingPointNode() { }

    public override NodeState Evaluate()
    {
        Roam();
        return _nodeState;
    }

    private void Roam()
    {
        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, MonsterAI.currentTargetDoor.transform);

        //Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 1 && currentRoom != MonsterAI.currentTargetRoom)
        {
            //pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;

            if (pathToTarget.Count == 1)
            {
                MonsterAI.currentTarget = pathToTarget[0].gameObject;
            }
            else if (pathToTarget[1].gameObject.GetComponent<DoorCollision>().room.gameObject == MonsterAI.currentEnemyRoom)
            {
                MonsterAI.currentTarget = pathToTarget[1].gameObject;
            }
            else
            {
                MonsterAI.currentTarget = pathToTarget[0].gameObject;
            }
            _nodeState = NodeState.RUNNING;
            return;
        }

        MonsterAI.currentTarget = MonsterAI.currentTargetRoaming;

        if (RoamingTarget.reachedRoamingTarget)
        {
            MonsterAI.currentTargetRoom = null;
            MonsterAI.currentTargetDoor = null;
            MonsterAI.currentTargetRoaming = null;
            _nodeState = NodeState.SUCCESS;
        }
    }
}
