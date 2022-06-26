using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMove : MonoBehaviour
{
    private Rigidbody2D rb2;
    public float moveSpeed;
    public static GameObject currentPlayerRoom;

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, Input.GetAxisRaw("Vertical") * moveSpeed);
    }
}
