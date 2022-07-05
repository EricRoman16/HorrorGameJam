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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            reachedRoamingTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            reachedRoamingTarget = false;
        }
    }
}
