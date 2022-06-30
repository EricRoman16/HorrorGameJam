using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingTarget : MonoBehaviour
{
    [HideInInspector]
    public GameObject roamingTargetRoom;

    private void Awake()
    {
        roamingTargetRoom = GetComponentInParent<RoomScript>().gameObject;
    }
}
