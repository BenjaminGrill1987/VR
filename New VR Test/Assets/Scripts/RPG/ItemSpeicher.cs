using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeicher : MonoBehaviour
{
    private GameObject item;

    public void GetItem(GameObject item)
    {
        this.item = item;
    }
}
