using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject canvas, player, enemy;
    private PlayerBinds playerBinds;

    public Button retryButton;

    private bool menuActive;

    private void Awake()
    {
        playerBinds = new PlayerBinds();
    }

    private void Update()
    {
        Death();
    }

    private void Death()
    {
        if (Player.isDead && menuActive == false)
        {
            menuActive = true;
            playerBinds.Player.Disable();
            canvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            retryButton.Select();
            //playerBinds.UI.Enable();
        }
    }

    public void Respawn()
    {
        player.GetComponent<Player>().Respawn();
        enemy.GetComponent<MonsterAI>().Respawn();
        Camera.main.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, Camera.main.transform.position.z);
        canvas.SetActive(false);
        playerBinds.Player.Enable();
        menuActive = false;
        //playerBinds.UI.Disable();
    }
}
