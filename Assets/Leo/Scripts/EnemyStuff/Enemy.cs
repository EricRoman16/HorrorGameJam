using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static GameObject currentRoom;
    public static GameObject target;
    public static State state = State.chasing;
    public static bool hasDirectSight;

    public enum State
    {
        roaming,
        alerted,
        chasing
    }

    public static void FindCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    public static void SetTarget(GameObject target)
    {
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(currentRoom.transform, target.transform);

        if (pathToTarget != null)
        {
            if (pathToTarget.Count > 0)  
            {
                if (pathToTarget[1].gameObject.name == "Door (2)")
                {

                }
                else
                {

                }
            }
        }
    }

    private static void CheckDirectSight()
    {

    }
}
