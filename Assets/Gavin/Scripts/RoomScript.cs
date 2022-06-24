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

    /// <summary>
    /// Returns the first door found in a given room to pass into GetPath
    /// </summary>
    /// <returns></returns>
    public GameObject GetDoorInRoom()
    {
        foreach (SpriteRenderer spriteRenderer in objectsInRoom)
        {
            if (spriteRenderer.gameObject.name.Contains("Door"))
            {
                return spriteRenderer.gameObject;
            }
        }
        return null;
    }

    // Sets the enemy's current room whenever it enters a new room
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enter");
            Enemy.SetCurrentRoom(gameObject);
            Enemy.SetTarget();
        }
    }
}
