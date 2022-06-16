using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public int roomID;
    public string roomName;
    public RoomType roomType;
    public Exit[] exitCount;
}

public enum RoomType
{
    normal,
    scripted,
    safe
}
