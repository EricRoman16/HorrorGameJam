using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForSecondsNode : Node
{
    private float stoptime;

    public StopForSecondsNode(float seconds) 
    {
        stoptime = seconds;
    }

    public override NodeState Evaluate()
    {
        stoptime -= Time.deltaTime;

        if (stoptime <= 0)
        {
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.RUNNING;
        }
    }
}
