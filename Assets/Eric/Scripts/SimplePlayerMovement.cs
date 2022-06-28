using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool nearCloset = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nearCloset)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(36, 238, 17, 1);//might need to change these values
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(17, 95, 238, .4f);//might need to change these values
        }

        if(nearCloset && Input.GetKeyDown(KeyCode.E))
        {
            EnterCloset();
        }
        
        
        
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);
    }

    public void EnterCloset()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(17, 95, 238, 0);//might need to change these values

        //Whatever else needs to happen
    }
}
