using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetScript : MonoBehaviour
{
    public Text KeyIndicator;
    private GameObject hidingTarget;
    private bool nearPlayer;

    private void Awake()
    {
        hidingTarget = GetComponentInChildren<HidingTarget>().gameObject;
        KeyIndicator.enabled = false;
    }

    private void Update()
    {
        ClosetCheck();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.nearCloset = true;
            nearPlayer = true;
            KeyIndicator.enabled = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.nearCloset = false;
            nearPlayer = false;
            KeyIndicator.enabled = false;
            Player.currentHidingSpot = null;
        }
    }

    private void ClosetCheck()
    {
        if (nearPlayer && Player.inCloset)
        {
            Player.currentHidingSpot = hidingTarget;
        }
    }
}
