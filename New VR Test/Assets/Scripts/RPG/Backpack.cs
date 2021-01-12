using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using NaughtyAttributes;

public class Backpack : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemSlots;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private GameObject slot;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private float xOffset, zOffset;
    [SerializeField] private int numberOfSlots;

    private bool isOpen = false;

    void Start()
    {
        Vector3 slotsPosition = firstPosition.position;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject slots;
            if (items != null && i < items.Count)
            {
                GameObject item;
                slots = Instantiate(slot, slotsPosition, slot.transform.rotation);
                slots.transform.parent = firstPosition;
                item = Instantiate(items[i], slotsPosition,Quaternion.identity);
                item.transform.parent = firstPosition;
                slots.GetComponent<XRSocketInteractor>().startingSelectedInteractable = item.GetComponent<XRGrabInteractable>();
                slotsPosition = new Vector3(slotsPosition.x + xOffset, slotsPosition.y, slotsPosition.z);
                itemSlots.Add(slots);
            }
            else
            {
                slots = Instantiate(slot, slotsPosition, slot.transform.rotation);
                slots.transform.parent = firstPosition;
                slotsPosition = new Vector3(slotsPosition.x + xOffset, slotsPosition.y, slotsPosition.z);
                itemSlots.Add(slots);
            }
        }
    }

    public void OpenClose()
    {
        isOpen = !isOpen;
        if(isOpen)
        {
            OpenBackpack();
        }
        else
        {
            CloseBackpack();
        }
    }

    private void OpenBackpack()
    {
        firstPosition.gameObject.SetActive(true);
        
        if(items.Count != 0)
        {
            for(int i = 0;i<items.Count;i++)
            {
                items[i].transform.parent = null;
            }
        }

    }

    private void CloseBackpack()
    {
        List<GameObject> copyItems = new List<GameObject>();

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].GetComponent<XRSocketInteractor>().selectTarget != null)
            {
                copyItems.Add(itemSlots[i].GetComponent<XRSocketInteractor>().selectTarget.gameObject);
                itemSlots[i].GetComponent<XRSocketInteractor>().selectTarget.gameObject.transform.parent = firstPosition;
            }
        }

        items = copyItems;

        firstPosition.gameObject.SetActive(false);
    }
}

public enum BackPackSize
{
    Small,
    Medium,
    Big,
    Endless
}