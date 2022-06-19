using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
    public GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Enemy.isChasing)
        {
            Enemy.CheckEvasion();
        }
        else
        {
            Enemy.CheckEncounter();
        }
    }
}
