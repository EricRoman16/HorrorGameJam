using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHidingSpotNode : Node
{
    private Transform currentPos;

    public ChooseHidingSpotNode(Transform currentPos) 
    { 
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        if (MonsterAI.targetHidingSpot == null)
        {
            MonsterAI.currentTarget = ChooseHidingSpot();
        }

        float dist = Vector3.Distance(currentPos.position, MonsterAI.currentTarget.transform.position);

        if (dist > 1)
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
        List<GameObject> hidingSpots = Player.currentPlayerRoom.GetComponent<RoomScript>().hidingTargets;

        int randomHidingSpot = Random.Range(0, hidingSpots.Count);

        return MonsterAI.targetHidingSpot = hidingSpots[randomHidingSpot];
    }
}
