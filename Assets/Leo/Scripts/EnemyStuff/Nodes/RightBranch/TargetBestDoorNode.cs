using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBestDoorNode : Node
{
    public TargetBestDoorNode() { }

    public override NodeState Evaluate()
    {
        //Debug.Log("Target Best Door Node");
        TargetBestDoor();
        return nodeState;
    }

    private void TargetBestDoor()
    {
        GameObject startingDoor = MonsterAI.currentEnemyRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, MonsterAI.currentTargetDoor.transform);

        //Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 1 && MonsterAI.currentEnemyRoom != MonsterAI.currentTargetRoom)
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
            //Debug.Log("Running");
            _nodeState = NodeState.RUNNING;
        }
        else
        {
            //Debug.Log("Success");
            _nodeState = NodeState.SUCCESS;
        }
    }
}
