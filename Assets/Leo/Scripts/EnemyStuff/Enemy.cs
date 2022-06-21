using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static int enemyRoomID;
    public static State state = State.chasing;
    public static bool hasDirectSight;

    public enum State
    {
        roaming,
        alerted,
        chasing
    }

    /// <summary>
    /// Increase chase progress if player is in the same room as the enemy
    /// </summary>
    private static void CheckDirectSight()
    {
        if (enemyRoomID == 0 /*Player current room ID*/)
        {
            hasDirectSight = true;
        }
        else
        {
            hasDirectSight = false;
        }
    }
}
