using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2;
    public float moveSpeed;
    public static bool playerHidden, inCloset, nearCloset, isDead;
    public static GameObject currentPlayerRoom, currentHidingSpot;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDead == false)
        {
            rb2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
        }
    }

    private void Update()
    {
        if (isDead == false)
        {
            ClosetCheck();
        }
    }

    private void ClosetCheck()
    {
        if (nearCloset)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (!inCloset)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }

        if ((nearCloset || inCloset) && Input.GetKeyDown(KeyCode.E))
        {
            ClosetInteract();
        }


        if (!inCloset)
        {
            rb2.velocity = new Vector2(0, 0);
        }
    }

    private void ClosetInteract()
    {
        if (!inCloset)
        {
            if (MonsterAI.playerSpotted)
            {
                playerHidden = false;
            }
            else
            {
                playerHidden = true;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (inCloset)
        {
            playerHidden = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

        inCloset = !inCloset;
    }

    public void Respawn()
    {
        if (inCloset)
        {
            ClosetInteract();
        }
        isDead = false;
        transform.position = new Vector3(-5, 5, -2);
    }
}
