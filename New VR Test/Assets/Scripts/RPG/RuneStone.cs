using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneStone : MonoBehaviour
{
    [SerializeField] private Magic runeMagic;
    [SerializeField] private ParticleSystem particleSystem;

    public void ActivateMagic()
    {
        particleSystem.Play();
    }

    public void DeactivateMagic()
    {
        particleSystem.Stop();
    }
}

public enum Magic
{
    Attack,
    Defense,
    Heal
}
