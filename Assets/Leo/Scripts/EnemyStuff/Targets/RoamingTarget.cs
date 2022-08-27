using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingTarget : MonoBehaviour
{
    [HideInInspector]
    public GameObject roamingTargetRoom;
    public static bool reachedRoamingTarget;

    private void Awake()
    {
        roamingTargetRoom = GetComponentInParent<RoomScript>().gameObject;
    }
}
