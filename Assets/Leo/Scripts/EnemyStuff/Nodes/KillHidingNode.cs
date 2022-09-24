using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillHidingNode : Node
{
    public KillHidingNode() { }

    public override NodeState Evaluate()
    {
        Player.isDead = true;
        return NodeState.SUCCESS;
    }
}
