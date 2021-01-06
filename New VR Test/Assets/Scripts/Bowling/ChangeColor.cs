using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] List<Material> mats;

    public void HoverChange()
    {
        gameObject.GetComponent<Renderer>().material = mats[0];
    }

    public void ChangeBack()
    {
        gameObject.GetComponent<Renderer>().material = mats[1];
    }

    public void Selected()
    {
        gameObject.GetComponent<Renderer>().material = mats[2];
    }

    public void ExitSelected()
    {
        gameObject.GetComponent<Renderer>().material = mats[1];
    }
}