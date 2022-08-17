using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTarget : MonoBehaviour
{
    [HideInInspector] public GameObject hidingTargetRoom;

    private void Awake()
    {
        hidingTargetRoom = GetComponentInParent<RoomScript>().gameObject;
    } 
}
