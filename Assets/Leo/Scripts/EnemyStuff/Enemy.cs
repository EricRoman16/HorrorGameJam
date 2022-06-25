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

    public static void SetTarget()
    {
        state = State.alerted;
        Debug.Log("alerted");
        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();

        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, currentFinalTarget.transform);

        if (pathToTarget != null && pathToTarget.Count > 2 && state != State.chasing)
        {
            pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;
            pathToTarget[1].GetComponent<SpriteRenderer>().color = Color.cyan;

            if (pathToTarget[1].gameObject.GetComponent<DoorCollision>().room.gameObject == currentRoom)
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
        Debug.Log("chasing");
        state = State.chasing;
    }

    private static void CheckDirectSight()
    {

    }
}
