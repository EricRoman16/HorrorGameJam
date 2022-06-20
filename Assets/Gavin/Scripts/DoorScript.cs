using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class DoorScript : MonoBehaviour
{
    public RoomScript[] connectedRooms;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="go">The gameObject that collided with the door</param>
    /// <param name="index">The index of the door that was collided with</param>
    public void DoorHit(GameObject go, int index)
    {
        if(go.name == "Player")
        {
            go.transform.position = transform.GetChild(1 - index).GetChild(0).position;
            transform.GetChild(1 - index).GetComponent<DoorCollision>().usable = false;
        }
    }

    void UpdateRooms()
    {
        if(connectedRooms == null || connectedRooms.Length < 2)
        {
            connectedRooms = new RoomScript[2];
        }
        
        if(connectedRooms[0] == null)
        {

            connectedRooms[0] = GetClosestRoom(connectedRooms[1]);
        }
        
        if(connectedRooms[1] == null)
        {
            connectedRooms[1] = GetClosestRoom(connectedRooms[0]);
        }
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

