using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    TextMeshProUGUI itemDesc;
    TextMeshProUGUI itemName;

    // Start is called before the first frame update
    void Start()
    {
        GameObject itemDescParent = GameObject.Find("ItemDescription");
        itemName = itemDescParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDesc = itemDescParent.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowItemData(ItemData data)
    {
        print(data.itemName + " : " + data.itemDescription);
        itemName.text = data.itemName;
        itemDesc.text = data.itemDescription;
    }
}
