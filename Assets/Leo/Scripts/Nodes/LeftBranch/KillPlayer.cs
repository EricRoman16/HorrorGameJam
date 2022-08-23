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
        //Add code
        //player.GetComponent<SpriteRenderer>().color = Color.black;
        return NodeState.SUCCESS;
    }
}
