using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject inventory, menu;

    private bool isInventory;

    private void Start()
    {
        isInventory = false;
    }

    public void OpenInventory()
    {
        if(!isInventory)
        {
            isInventory = true;
            inventory.SetActive(true);
            menu.SetActive(false);
            gameObject.GetComponent<InventorySystem>().SetItems();
        }
    }

    public void CloseInventory()
    {
        if(isInventory)
        {
            isInventory = false;
            inventory.SetActive(false);
            menu.SetActive(true);
        }
    }
}