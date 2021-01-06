using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField] private float degreeBorder;
    [SerializeField] private GameObject pin;

    private Aufstellung aufstellung;
    private bool notFallen = true;

    public bool NotFallen { get => notFallen;}

    private void FixedUpdate()
    {
        if(CheckAngle() && notFallen)
        {
            notFallen = false;
            aufstellung.DeletePin();
        }
    }

    public bool CheckAngle()
    {
        return Vector3.Angle(pin.transform.up, Vector3.up) > degreeBorder;
    }

    public void SetOptions(Aufstellung newAufstellung)
    {
        aufstellung = newAufstellung;
    }
}