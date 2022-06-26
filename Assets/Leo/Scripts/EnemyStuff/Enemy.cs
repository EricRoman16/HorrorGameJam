using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static GameObject currentRoom;
    public static GameObject currentTarget;
    public static GameObject currentFinalTarget;
    public static State state = State.roaming;
    public static bool hasDirectSight;

    public enum State
    {
        roaming,
        alerted,
        chasing
    }

    public static void SetCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    /// <summary>
    /// Sets the enemy's target to a door leading to the player according to GetPath in RoomPathFinder. 
    /// If the player is in the same room it will retarget to the player 
    /// </summary>
    public static void SetTarget()
    {
        state = State.alerted;
        //Debug.Log("alerted");

        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, currentFinalTarget.transform);

        //Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 0 && currentRoom != TempMove.currentPlayerRoom)
        {
            //pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;

            if (pathToTarget.Count == 1)
            {
                currentTarget = pathToTarget[0].gameObject;
                return;
            }
            else if (pathToTarget[1].gameObject.GetComponent<DoorCollision>().room.gameObject == currentRoom)
            {
                currentTarget = pathToTarget[1].gameObject;
                return;
            }
            else
            {
                currentTarget = pathToTarget[0].gameObject;
                return;
            }
        }
        state = State.chasing;
        //Debug.Log("chasing");
    }

    public static void TargetRoaming()
    {
        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, currentFinalTarget.transform);

        //Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 1 && currentRoom != TempMove.currentPlayerRoom)
        {
            //pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;

            if (pathToTarget.Count == 1)
            {
                currentTarget = pathToTarget[0].gameObject;
                return;
            }
            else if (pathToTarget[1].gameObject.GetComponent<DoorCollision>().room.gameObject == currentRoom)
            {
                currentTarget = pathToTarget[1].gameObject;
                return;
            }
            else
            {
                currentTarget = pathToTarget[0].gameObject;
                return;
            }
        }
        SetRoamingTarget();
    }

    public static void SetRoamingTarget()
    {
        GameObject targetRoom = RoomScript.rooms[Random.Range(0, RoomScript.rooms.Count)];
        currentFinalTarget = targetRoom.GetComponent<RoomScript>().GetDoorInRoom();
        Debug.Log(RoomScript.rooms.Count);

        currentFinalTarget.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
