using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private FireState currentState;
    [SerializeField] private ParticleSystem firePartical;

    public FireState CurrentState { get => currentState; }

    private void Start()
    {
        if (currentState == FireState.Light)
        {
            firePartical.Play();
        }
        else
        {
            firePartical.Stop();
        }
    }

    private void TryToChange(FireState newState)
    {
        bool permissionGranted = false;

        if (currentState != newState)
        {
            permissionGranted = true;
        }

        switch (newState)
        {
            case FireState.Light:
                {
                    if (permissionGranted)
                    {
                        firePartical.Play();
                        currentState = newState;
                    }
                    break;
                }
            case FireState.Extinguished:
                {
                    if (permissionGranted)
                    {
                        firePartical.Stop();
                        currentState = newState;
                    }
                    break;
                }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider.gameObject.tag == "TorchHead" && contact.otherCollider.gameObject.tag == "TorchHead" && collision.gameObject.GetComponent<Torch>().CurrentState == FireState.Light)
            {
                TryToChange(FireState.Light);
            }
        }
    }
}

public enum FireState
{
    Light,
    Extinguished
}