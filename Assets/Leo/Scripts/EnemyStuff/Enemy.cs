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
        Debug.Log("SetTarget called");
        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, currentFinalTarget.transform);

        if (pathToTarget != null && pathToTarget.Count > 0)
        {
            Debug.Log("Path found");

            if (pathToTarget[1].gameObject.name == "Door (1)")
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
        state = State.roaming;
    }

    private static void CheckDirectSight()
    {

    }
}
