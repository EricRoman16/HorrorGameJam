using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour
{ 
    public RoomScript room;
    private DoorScript door;
    public static GameObject alertedRoom;
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
        if (collision.tag == "Player")
        {
            if (door.makesNoise)
            {
                alertedRoom = transform.parent.gameObject.transform.GetChild(1 - transform.GetSiblingIndex()).GetComponent<DoorCollision>().room.gameObject;
                //alertedRoom.GetComponent<SpriteRenderer>().color = Color.magenta;
                MonsterAI.alerted = true;
            }
            door.DoorHit(collision.gameObject, transform.GetSiblingIndex());
        }
        else if (collision.tag == "Enemy")
        {
            door.DoorHit(collision.gameObject, transform.GetSiblingIndex());
        }
    }

    private void FindSibling()
    {
        alertedRoom = transform.parent.gameObject.transform.GetChild(1 - transform.GetSiblingIndex()).gameObject.GetComponent<RoomScript>().gameObject;
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
