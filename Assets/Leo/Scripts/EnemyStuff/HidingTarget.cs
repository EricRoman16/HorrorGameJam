using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingTarget : MonoBehaviour
{
    [HideInInspector]
    public GameObject hidingTargetRoom;
    public static bool reachedHidingTarget;

    private void Awake()
    {
        hidingTargetRoom = GetComponentInParent<RoomScript>().gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            reachedHidingTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            reachedHidingTarget = false;
        }
    }
}
