﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsHidingNode : Node
{
    public IsHidingNode() { }

    public override NodeState Evaluate()
    {
        if (Player.inCloset == false)
        {
            return NodeState.FAILURE;
        }
        else
        {
            return NodeState.SUCCESS;
        }
    }
}
