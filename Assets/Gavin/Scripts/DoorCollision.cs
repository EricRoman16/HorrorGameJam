using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollision : MonoBehaviour
{
    
    public RoomScript room;
    DoorScript door;
    public bool usable;
    // Start is called before the first frame update
    void Start()
    {
        door = transform.parent.GetComponent<DoorScript>();
        usable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usable)
        {
            door.DoorHit(collision.gameObject, transform.GetSiblingIndex());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        usable = true;
    }
}
