using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static int enemyRoomID;
    public static bool isChasing;
    public static bool hasDirectSight;
    public static float chaseProgress;

    /// <summary>
    /// Increase chase progress and check if encountered 
    /// </summary>
    public static void CheckEncounter()
    {
        chaseProgress += Random.Range(1, 3);

        if (chaseProgress >= 10)
        {
            isChasing = true;
        }
    }

    /// <summary>
    /// Decrease chase progress and check if successfully evaded 
    /// </summary>
    public static void CheckEvasion()
    {
        chaseProgress -= Random.Range(3, 5);

        if (chaseProgress <= 0)
        {
            isChasing = false;
        }
    }

    /// <summary>
    /// Increase chase progress if player is in the same room as the enemy
    /// </summary>
    private static void CheckDirectSight()
    {
        if (enemyRoomID == 0 /*Player current room ID*/)
        {
            hasDirectSight = true;
            chaseProgress += 0.5f * Time.deltaTime;
        }
        else
        {
            hasDirectSight = false;
        }
    }
}
