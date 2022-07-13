using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DoorScript : MonoBehaviour
{
    public static GameObject lastSeenDoor;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go">The gameObject that collided with the door</param>
    /// <param name="index">The index of the door that was collided with</param>
    public void DoorHit(GameObject go, int index)
    {
        if(go.name == "Player")
        {
            StartCoroutine(DelayMove(go, index));
            Camera.main.GetComponent<TransitionScript>().StartTransition();
            if (Enemy.sight == Enemy.DirectSight.hasSight)
            {
                //Debug.Log("storing door");
                lastSeenDoor = transform.GetChild(index).gameObject;
            }
            //Enemy.finalTarget = transform.GetChild(index).gameObject;
        }
        if (go.name == "Enemy")
        {
            go.transform.position = transform.GetChild(1 - index).GetChild(0).position;
            //transform.GetChild(1 - index).GetComponent<DoorCollision>().enemyUsable = false;
        }
    }

    IEnumerator DelayMove(GameObject go, int index)
    {
        yield return null; 
        yield return null; 
        go.transform.position = transform.GetChild(1 - index).GetChild(0).position;
        //transform.GetChild(1 - index).GetComponent<DoorCollision>().playerUsable = false;
        RoomPathfinder.currentPathfinder.UpdatePath(GameObject.Find("Door").transform.GetChild(0), transform.GetChild(1 - index));
        transform.GetChild(1 - index).GetComponent<DoorCollision>().room.visible = true;
        transform.GetChild(index).GetComponent<DoorCollision>().room.visible = false;


        Camera.main.transform.position = transform.GetChild(1 - index).GetComponent<DoorCollision>().room.transform.position;
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -10);

    }

    RoomScript GetClosestRoom(RoomScript ignoreRoom)
    {
        RoomScript[] rooms = transform.parent.GetComponentsInChildren<RoomScript>();
        if(rooms.Length == 0)
        {
            return null;
        }

        RoomScript closestRoom = rooms[0] == ignoreRoom ? rooms[1] : rooms[0];
        float dist = Vector2.Distance(closestRoom.transform.position, transform.position);
        for (int i = 0; i < rooms.Length; i++)
        {
            RoomScript newRoom = rooms[i];
            if(newRoom == ignoreRoom)
            {
                continue;
            }
            float newDist = Vector2.Distance(newRoom.transform.position, transform.position);
            if (newDist < dist)
            {
                dist = newDist;
                closestRoom = newRoom;
            }
        }
        return closestRoom;
    }
}

