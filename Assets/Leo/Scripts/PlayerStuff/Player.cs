using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2;
    private PlayerBinds playerBinds;
    public float moveSpeed;
    public static bool playerHidden, inCloset, nearCloset, isDead;
    public static GameObject currentPlayerRoom, currentHidingSpot;

    private Animator ani;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        playerBinds = new PlayerBinds();
        playerBinds.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (isDead == false && inCloset == false)
        {
            rb2.velocity = moveDirection * moveSpeed;
            ani.SetFloat("Magnitude", rb2.velocity.magnitude);
        }
    }

    private void Update()
    {
        if (isDead == false)
        {
            ClosetCheck();
            if (inCloset == false)
            {
                CheckMoveInput();
                AnimatePlayer();
            }
        }
    }

    private void CheckMoveInput()
    {
        float moveX = playerBinds.Player.Move.ReadValue<Vector2>().x;
        float moveY = playerBinds.Player.Move.ReadValue<Vector2>().y;

        if ((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0)
        {
            lastMoveDirection = moveDirection;
        }

        moveDirection = playerBinds.Player.Move.ReadValue<Vector2>();
    }

    private void AnimatePlayer()
    {
        float moveX = playerBinds.Player.Move.ReadValue<Vector2>().x;
        float moveY = playerBinds.Player.Move.ReadValue<Vector2>().y;

        ani.SetFloat("AnimMoveX", moveX);
        ani.SetFloat("AnimMoveY", moveY);
        ani.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        ani.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    }

    private void ClosetCheck()
    {
        if (nearCloset)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else if (!inCloset)
        {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
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
