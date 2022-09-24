using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfSameRoomNode : Node
{
    public CheckIfSameRoomNode() { }

    public override NodeState Evaluate()
    {
        if (MonsterAI.currentEnemyRoom == Player.currentPlayerRoom)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
