using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private bool isSpawned;

    private void Update()
    {
        if (Enemy.isChasing && isSpawned == false)
        {
            SpawnEnemy();
        }
        else if (Enemy.isChasing == false && isSpawned == true)
        {
            DespawnEnemy();
        }
        Debug.Log(Enemy.chaseProgress);
    }

    private void SpawnEnemy()
    {
        isSpawned = true;
        enemy.transform.position = Vector3.zero;
        enemy.SetActive(true);
    }

    private void DespawnEnemy()
    {
        isSpawned = false;
        enemy.SetActive(false);
    }
}
