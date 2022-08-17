using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHidingSpotNode : Node
{
    private GameObject currentRoom;
    private Transform currentPos;

    public ChooseHidingSpotNode(GameObject currentEnemyRoom, Transform currentPos) 
    { 
        currentRoom = currentEnemyRoom;
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        MonsterAI.currentTarget = ChooseHidingSpot();

        float dist = Vector3.Distance(currentPos.position, ChooseHidingSpot().transform.position);

        if (dist > 3)
        {
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }

    private GameObject ChooseHidingSpot()
    {
        List<GameObject> hidingSpots = currentRoom.GetComponent<RoomScript>().hidingTargets;

        int randomHidingSpot = Random.Range(0, hidingSpots.Count);

        return hidingSpots[randomHidingSpot];
    }
}
