using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enemy
{
    public static float speed;
    public static float chaseSpeed = 150;
    public static float roamSpeed = 50;

    public static GameObject currentRoom;
    public static GameObject currentTarget;
    public static GameObject finalTarget;
    public static GameObject finalObjective;

    public static bool playerHidden = false;
    public static Mode mode = Mode.roaming;
    public static RoamingState state = RoamingState.idle;
    public static DirectSight sight = DirectSight.noSight;

    public enum DirectSight
    {
        noSight,
        hasSight,
        hadSight
    }

    public enum Mode
    {
        roaming,
        searching,
        checking,
        chasing
    }

    public enum RoamingState
    {
        idle,
        headingToTarget,
        foundTarget
    }

    public static void SetCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    /// <summary>
    /// Sets the enemy's mode to chasing
    /// </summary>
    public static void ChaseMode()
    {
        mode = Mode.chasing;
        //Debug.Log("chasing");
    }

    /// <summary>
    /// Sets the enemy's mode to searching
    /// </summary>
    public static void SearchMode()
    {
        mode = Mode.searching;
        //Debug.Log("searching");
    }


    public static void ChaseTarget()
    {
        state = RoamingState.headingToTarget;

        GameObject startingDoor = currentRoom.GetComponent<RoomScript>().GetDoorInRoom();
        List<Transform> pathToTarget = RoomPathfinder.currentPathfinder.GetPath(startingDoor.transform, finalTarget.transform);

        //Debug.Log(pathToTarget.Count);

        if (pathToTarget != null && pathToTarget.Count > 1 && currentRoom != ReturnTargetRoom(finalObjective))
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
        currentTarget = finalObjective;
        state = RoamingState.foundTarget;
    }

    /// <summary>
    /// Sets the target to a roaming waypoint in a random room and calls ChaseTarget
    /// </summary>
    public static void SetRoamingTarget()
    {
        //Debug.Log("Setting roaming target");
        GameObject targetRoom = RoomScript.rooms[Random.Range(0, RoomScript.rooms.Count)];
        finalTarget = targetRoom.GetComponent<RoomScript>().GetDoorInRoom();
        finalObjective = targetRoom.GetComponent<RoomScript>().roamingTarget;
        state = RoamingState.headingToTarget;
        mode = Mode.roaming;

        ChaseTarget();
    }

    /// <summary>
    /// Sets the target to the roaming target in the room 
    /// </summary>
    public static void TargetCurrentRoaming()
    {
        //Debug.Log("Setting hiding target");
        currentTarget = currentRoom.GetComponent<RoomScript>().roamingTarget;
        state = RoamingState.foundTarget;
    }

    /// <summary>
    /// Chooses a raondom hiding spot on the room and goes to it
    /// </summary>
    public static void ChooseRandomHidden()
    {
        //Debug.Log("Setting hiding target");
        //currentTarget = currentRoom.GetComponent<RoomScript>().hidingTarget;
        mode = Mode.checking;
        state = RoamingState.foundTarget;
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
            return Player.currentPlayerRoom;
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

    public static void CheckLineOfSight()
    {
        if (currentRoom == Player.currentPlayerRoom)
        {
            //Debug.Log("true");
            sight = DirectSight.hasSight;
        }
        else if (sight == DirectSight.hasSight)
        {
            sight = DirectSight.hadSight;
        }
        else
        {
            //Debug.Log("false");
            sight = DirectSight.noSight;
        }
    }

    public static void CheckPlayerLineOfSight()
    {
        if (currentRoom == Player.currentPlayerRoom)
        {
            sight = DirectSight.hasSight;
        }
        else if (sight == DirectSight.hasSight)
        {
            sight = DirectSight.hadSight;
        }
        //else
        //{
        //    sight = DirectSight.noSight;
        //}
    }

    public static void SetSpeed()
    {
        if (mode == Mode.roaming)
        {
            speed = roamSpeed;
        }
        else
        {
            speed = chaseSpeed;
        }
    }
}
