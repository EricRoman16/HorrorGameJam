using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerDeadNode : Node
{
    private Rigidbody2D rb;

    public IsPlayerDeadNode(Rigidbody2D rb) 
    {
        this.rb = rb;
    }

    public override NodeState Evaluate()
    {
        if (Player.isDead == false)
        {
            return NodeState.FAILURE;
        }
        else
        {
            rb.velocity = Vector2.zero;
            return NodeState.SUCCESS;
        }
    }
}
