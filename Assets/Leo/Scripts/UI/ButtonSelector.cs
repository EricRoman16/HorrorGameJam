using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelector : MonoBehaviour
{
    private Transform currentButton;


    void Update()
    {
        SetButton();
        transform.SetPositionAndRotation(new Vector3 (currentButton.position.x - 100, currentButton.position.y, 0), transform.rotation);
    }

    private void SetButton()
    {
        currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Transform>();
    }
}
