using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopForSecondsNode : Node
{
    private float roamSpeed;
    private static float stoptime = 2;
    private Rigidbody2D rb;
    public static bool done;

    public StopForSecondsNode(float roamSpeed, Rigidbody2D rb)
    {
        this.roamSpeed = roamSpeed;
        this.rb = rb;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("Stop For Seconds Node");

        stoptime -= Time.deltaTime;

        if (stoptime <= 0 || done)
        {
            //Debug.Log("SUCCESS");
            MonsterAI.currentTargetRoom = null;
            MonsterAI.currentTargetDoor = null;
            MonsterAI.currentTargetRoaming = null;

            stoptime = 2;
            MonsterAI.currentSpeed = roamSpeed;
            done = true;
            return NodeState.SUCCESS;
        }
        else
        {
            //Debug.Log("running");
            MonsterAI.currentSpeed = 0;
            rb.velocity = Vector2.zero;
            return NodeState.RUNNING;
        }
    }
}
