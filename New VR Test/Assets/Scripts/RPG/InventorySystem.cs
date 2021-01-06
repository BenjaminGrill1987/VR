using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] List<GameObject> items;
    [SerializeField] List<ItemSpeicher> itemsSlots;

    public void SetItems()
    {
        for(int i = 0;i<items.Count;i++)
        {
            itemsSlots[i].GetItem(items[i]);
        }
    }
}