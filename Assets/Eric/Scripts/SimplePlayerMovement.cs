using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool nearCloset = false;
    public bool inCloset = false;
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
        else if(!inCloset)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Vector4(17, 95, 238, .4f);//might need to change these values
        }

        if((nearCloset || inCloset) && Input.GetKeyDown(KeyCode.E))
        {
            Closet();
        }
        
        
        if(!inCloset)
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);
    }
    
    public void Closet()
    {
        if (!inCloset)
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(17, 95, 238, 0);//might need to change these values
        if (inCloset)
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //gameObject.GetComponent<SpriteRenderer>().color = new Vector4(17, 95, 238, 1);//might need to change these values
        inCloset = !inCloset;
        //this.transform.position += new Vector3(0, 5, 0);
        //Whatever else needs to happen
    }
}
