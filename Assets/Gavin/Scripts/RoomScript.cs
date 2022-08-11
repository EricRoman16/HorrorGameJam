﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public List<SpriteRenderer> objectsInRoom = new List<SpriteRenderer>();
    public static List<GameObject> rooms = new List<GameObject>();
    [HideInInspector] public GameObject roamingTarget, hidingTarget;
    public bool visible = false;

    private void Awake()
    {
        rooms.Add(gameObject);
        roamingTarget = GetComponentInChildren<RoamingTarget>().gameObject;
        hidingTarget = GetComponentInChildren<HidingTarget>().gameObject;
    }

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
            //spriteRenderer.enabled = visible;
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
            if (spriteRenderer.gameObject.tag == "Door")
            {
                return spriteRenderer.gameObject;
            }
        }
        return null;
    }

    // Sets the enemy or player's current room whenever they enter a new room and makes the enemy retarget
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("enter");
            Enemy.SetCurrentRoom(gameObject);
            Enemy.CheckLineOfSight();
            if (Enemy.currentRoom == Player.currentPlayerRoom)
            {
                Enemy.ChaseMode();
            }
            else if (Enemy.mode == Enemy.Mode.chasing && Enemy.sight == Enemy.DirectSight.noSight)
            {
                Enemy.SetRoamingTarget();
            }
            else if (Enemy.state != Enemy.RoamingState.idle)
            {
                Enemy.ChaseTarget();
            }
            Enemy.SetSpeed();
        }
        if (collision.gameObject.tag == "Player")
        {
            Player.currentPlayerRoom = gameObject;
            Enemy.CheckPlayerLineOfSight();
            Enemy.SetSpeed();
            //Enemy.ChaseTarget();
        }
    }
}
