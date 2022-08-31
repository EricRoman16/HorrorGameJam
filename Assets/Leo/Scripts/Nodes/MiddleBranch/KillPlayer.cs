using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : Node
{
    private GameObject player;

    public KillPlayer(GameObject player) 
    { 
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        Player.isDead = true;
        return NodeState.SUCCESS;
    }
}
