using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotSpottedNode : Node
{
    public NotSpottedNode() { }

    public override NodeState Evaluate()
    {
        //Debug.Log("Not Spotted Node");
        if (MonsterAI.playerSpotted == false)
        {
            //Debug.Log("Not Spotted");
            return NodeState.SUCCESS;
        }
        else
        {
            //Debug.Log("Spotted");
            MonsterAI.chasingPlayer = true;
            return NodeState.FAILURE;
        }
    }
}
