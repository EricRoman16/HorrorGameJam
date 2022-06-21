using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public List<SpriteRenderer> objectsInRoom = new List<SpriteRenderer>();
    public bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        objectsInRoom.Add(GetComponent<SpriteRenderer>());
        if(gameObject.name == "Room (2)")
        {
            visible = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpriteRenderer spriteRenderer in objectsInRoom)
        {
            spriteRenderer.enabled = visible;
        }
    }
}
