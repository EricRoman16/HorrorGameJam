using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas, player, enemy;


    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if (Player.isDead)
        {
            canvas.SetActive(true);
        }
    }

    public void Respawn()
    {
        player.GetComponent<Player>().Respawn();
        enemy.GetComponent<MonsterAI>().Respawn();
        canvas.SetActive(false);
    }
}
