using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static GameObject currentRoom;
    public static GameObject currentTarget;
    public static GameObject finalTarget;
    public static GameObject finalObjective;
    public static Mode mode = Mode.roaming;
    public static State state = State.headingToTarget;
    public static bool hasDirectSight;

    public enum Mode
    {
        roaming,
        alerted,
        chasing
    }

    public enum State
    {
        headingToTarget,
        foundTarget
    }

    public static void SetCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    /// <summary>
    /// Sets the enemy's target to a door leading to the player according to GetPath in RoomPathFinder. 
    /// If the player is in the same room it will retarget to the player 
    /// </summary>
    public static void ChaseTarget()
    {
        //Debug.Log("alerted");
        //mode = Mode.alerted;

        //GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        //List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, finalTarget.transform);

        //Debug.Log(pathToTarget.Count);

        //if (pathToTarget != null && pathToTarget.Count > 0 && currentRoom != ReturnTargetRoom(finalObjective))
        //{
        //    //pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;

        //    if (pathToTarget.Count == 1)
        //    {
        //        currentTarget = pathToTarget[0].gameObject;
        //        return;
        //    }
        //    else if (pathToTarget[1].gameObject.GetComponent<DoorCollision>().room.gameObject == currentRoom)
        //    {
        //        currentTarget = pathToTarget[1].gameObject;
        //        return;
        //    }
        //    else
        //    {
        //        currentTarget = pathToTarget[0].gameObject;
        //        return;
        //    }
        //}
        //mode = Mode.chasing;
        //Debug.Log("chasing");
    }

    public static void TargetRoaming()
    {
        state = State.headingToTarget;

        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, finalTarget.transform);

        Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 1 && currentRoom != ReturnTargetRoom(finalObjective))
        {
            pathToTarget[0].GetComponent<SpriteRenderer>().color = Color.green;

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
        currentTarget = finalObjective;
        state = State.foundTarget;
    }

    /// <summary>
    /// Sets the target to a roaming waypoint in a random room and calls ChaseTarget
    /// </summary>
    public static void SetRoamingTarget()
    {
        Debug.Log("Setting roaming target");

        GameObject targetRoom = RoomScript.rooms[Random.Range(0, RoomScript.rooms.Count)];
        finalTarget = targetRoom.GetComponent<RoomScript>().GetDoorInRoom();
        finalObjective = targetRoom.GetComponent<RoomScript>().roamingTarget;

        finalTarget.GetComponent<SpriteRenderer>().color = Color.red;
        finalObjective.GetComponent<SpriteRenderer>().color = Color.red;

        TargetRoaming();
    }

    /// <summary>
    /// Returns the current room of the target type (Player or Roaming Waypoint)
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private static GameObject ReturnTargetRoom(GameObject target)
    {
        if (target.tag == "Player")
        {
            return TempMove.currentPlayerRoom;
        }
        else if (target.tag == "Target")
        {
            return target.GetComponent<RoamingTarget>().roamingTargetRoom;
        }
        else
        {
            Debug.Log("Target Unknown");
            return null;
        }
    }
}
