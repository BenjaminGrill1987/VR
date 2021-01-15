using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour,IFlamable
{
    [SerializeField] private FireState currentState;
    [SerializeField] private ParticleSystem firePartical;

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

    public void TryToChange(FireState newState)
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

    private void OnParticleCollision(GameObject other)
    {
        Debug.LogError("Partical collision");
        if(other.gameObject.tag == "fire")
        {
            TryToChange(FireState.Light);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogError("normal collision");


        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.thisCollider.gameObject.tag == "TorchHead" && contact.otherCollider.gameObject.tag == "TorchHead" && collision.gameObject.GetComponent<IFlamable>().GetTheFireState() == FireState.Light)
            {
                TryToChange(FireState.Light);
            }
        }
    }

    public FireState GetTheFireState()
    {
        return currentState;
    }
}

public enum FireState
{
    Light,
    Extinguished
}