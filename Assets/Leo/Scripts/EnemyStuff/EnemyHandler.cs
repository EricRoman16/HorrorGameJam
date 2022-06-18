using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private void Update()
    {
        if (Enemy.isChasing)
        {
            CheckDirectSight();
        }
    }

    /// <summary>
    /// Increase chase progress and check if encountered 
    /// </summary>
    public void CheckEncounter()
    {
        Enemy.chaseProgress += Random.Range(0.5f, 3);

        if (Enemy.chaseProgress >= 10)
        {
            Enemy.isChasing = true;
        }
    }

    /// <summary>
    /// Decrease chase progress and check if successfully evaded 
    /// </summary>
    public void CheckEvasion()
    {
        Enemy.chaseProgress -= Random.Range(3, 4);

        if (Enemy.chaseProgress <= 0)
        {
            Enemy.isChasing = false;
        }
    }

    /// <summary>
    /// Increase chase progress if player is in the same room as the enemy
    /// </summary>
    private void CheckDirectSight()
    {
        if (Enemy.enemyRoomID == 0 /*Player current room ID*/)
        {
            Enemy.hasDirectSight = true;
            Enemy.chaseProgress += 0.5f * Time.deltaTime;
        }
        else
        {
            Enemy.hasDirectSight = false;
        }
    }
}
