using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour
{ 
    public RoomScript room;
    DoorScript door;
    //public bool playerUsable;
    //public bool enemyUsable;

    void Start()
    {
        door = transform.parent.GetComponent<DoorScript>();
        //playerUsable = true;
        //enemyUsable = true;
        room.objectsInRoom.Add(GetComponent<SpriteRenderer>());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if ((playerUsable && collision.tag == "Player") || (enemyUsable && collision.tag == "Enemy"))
        //{
        //    door.DoorHit(collision.gameObject, transform.GetSiblingIndex());
        //}
        if ((collision.tag == "Player") || (collision.tag == "Enemy"))
        {
            door.DoorHit(collision.gameObject, transform.GetSiblingIndex());
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        playerUsable = true;
    //    }
    //    else if(collision.tag == "Enemy")
    //    {
    //        enemyUsable = true;
    //    }
    //}
}
