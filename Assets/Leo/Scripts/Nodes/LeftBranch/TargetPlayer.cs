using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : Node
{
    private GameObject player;
    private Transform currentPos;

    public TargetPlayer(GameObject player, Transform currentPos) 
    {
        this.player = player;
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        MonsterAI.currentTarget = player;

        float dist = Vector3.Distance(currentPos.position, Player.currentHidingSpot.transform.position);

        if (dist > 3)
        {
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
