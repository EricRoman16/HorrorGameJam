using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public List<SpriteRenderer> objectsInRoom = new List<SpriteRenderer>();
    public bool visible = false;


    private void Start()
    {
        objectsInRoom.Add(GetComponent<SpriteRenderer>());
        if(gameObject.name == "Room (2)")
        {
            visible = true;
        }
    }

    private void Update()
    {
        foreach (SpriteRenderer spriteRenderer in objectsInRoom)
        {
            spriteRenderer.enabled = visible;
        }
    }

    private bool FindDoorsInRoom()
    {
        foreach (SpriteRenderer spriteRenderer in objectsInRoom)
        {
            if (spriteRenderer.gameObject.name == "Door (1)")
            {

            }
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enter");
            Enemy.FindCurrentRoom(gameObject);
        }
    }
}
