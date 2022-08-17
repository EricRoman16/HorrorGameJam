using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindHidingPlayerNode : Node
{
    public FindHidingPlayerNode() { }

    public override NodeState Evaluate()
    {
        //Add Code
        return NodeState.RUNNING;
    }
}
