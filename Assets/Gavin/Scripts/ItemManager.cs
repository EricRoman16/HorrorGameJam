using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    TextMeshProUGUI itemDesc;
    TextMeshProUGUI itemName;

    [System.Serializable]
    public class ItemInfo
    {
        public string name;
        public string description;
        public Sprite icon;
    }

    [SerializeField]
    ItemInfo[] itemInfo;

    List<ItemInfo> acquiredItems;

    GameObject[] itemSlots;


    // Start is called before the first frame update
    void Start()
    {
        acquiredItems = new List<ItemInfo>();
        
        itemSlots = GameObject.FindGameObjectsWithTag("ItemSlot");

        foreach (ItemInfo item in itemInfo)
        {
            acquiredItems.Add(item);
        }
        print(itemSlots.Length);
        print(acquiredItems.Count);

        GameObject itemDescParent = GameObject.Find("ItemDescription");
        itemName = itemDescParent.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        itemDesc = itemDescParent.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        UpdateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInventory()
    {
        for (int i = 0; i < acquiredItems.Count; i++)
        {
            if(i >= itemSlots.Length)
            {
                break;
            }
            itemSlots[i].GetComponentInChildren<Image>().sprite = acquiredItems[i].icon;
            itemSlots[i].GetComponent<ItemData>().itemName = acquiredItems[i].name;
            itemSlots[i].GetComponent<ItemData>().itemDescription = acquiredItems[i].description;
        }
    }

    public void ShowItemData(ItemData data)
    {
        print(data.itemName + " : " + data.itemDescription);
        itemName.text = data.itemName;
        itemDesc.text = data.itemDescription;
    }
}
