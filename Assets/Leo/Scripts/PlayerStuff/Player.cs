using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2;
    public float moveSpeed;
    public static bool playerHidden, playerHiding;
    public static GameObject currentPlayerRoom, currentHidingSpot;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;

            playerHiding = true;
            if (MonsterAI.playerSpotted)
            {
                playerHidden = false;
            }
            else
            {
                playerHidden = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;

            playerHiding = false;
            playerHidden = false;
        }
    }
}
