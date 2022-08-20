using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : Node
{
    private float chaseSpeed;
    private GameObject player;
    private Transform currentPos;

    public TargetPlayer(float chaseSpeed, GameObject player, Transform currentPos) 
    {
        this.chaseSpeed = chaseSpeed;
        this.player = player;
        this.currentPos = currentPos;
    }

    public override NodeState Evaluate()
    {
        MonsterAI.currentSpeed = chaseSpeed;
        MonsterAI.currentTarget = player;

        float dist = Vector3.Distance(currentPos.position, player.transform.position);

        if (dist > 1)
        {
            return NodeState.RUNNING;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
