using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetScript : MonoBehaviour
{
    public Text KeyIndicator;

    // Start is called before the first frame update
    void Start()
    {
        KeyIndicator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<SimplePlayerMovement>().nearCloset = true;
            KeyIndicator.enabled = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<SimplePlayerMovement>().nearCloset = false;
            KeyIndicator.enabled = false;
        }
    }
}
