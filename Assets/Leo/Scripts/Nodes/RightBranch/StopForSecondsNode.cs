using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForSecondsNode : Node
{
    private float roamSpeed;
    private static float stoptime = 2;

    public StopForSecondsNode(float roamSpeed)
    {
        this.roamSpeed = roamSpeed;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Stop For Seconds Node");

        stoptime -= Time.deltaTime;

        if (stoptime <= 0)
        {
            MonsterAI.currentTargetRoom = null;
            MonsterAI.currentTargetDoor = null;
            MonsterAI.currentTargetRoaming = null;

            stoptime = 2;
            MonsterAI.currentSpeed = roamSpeed;
            return NodeState.SUCCESS;
        }
        else
        {
            //Debug.Log("Stop For Seconds Node");
            MonsterAI.currentSpeed = 0;
            return NodeState.RUNNING;
        }
    }
}
