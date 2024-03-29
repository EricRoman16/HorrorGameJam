﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpottedNode : Node
{
    public SpottedNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.playerSpotted == false)
        {
            //Debug.Log("Not Spotted");
            return NodeState.FAILURE;
        }
        else
        {
            //Debug.Log("Spotted");
            StopForSecondsNode.done = false;
            MonsterAI.targetHidingSpot = null;
            MonsterAI.alerted = false;
            DoorCollision.alertedRoom = null;
            MonsterAI.chasingPlayer = true;
            return NodeState.SUCCESS;
        }
    }
}
