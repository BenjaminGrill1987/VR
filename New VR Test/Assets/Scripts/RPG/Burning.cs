﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour
{
    [SerializeField] private ParticleSystem firePartical;


    private void OnParticleCollision(GameObject other)
    {
            if (other.gameObject.tag == "fire")
            {
                ParticleSystem fire = Instantiate(firePartical, transform.position, Quaternion.Euler(270, 0, 0));
                fire.Play();
                fire.transform.parent = gameObject.transform;
                fire.transform.localScale = new Vector3(1, 1, 1);
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.tag == "TorchHead" && collision.gameObject.GetComponent<IFlamable>().GetTheFireState() == FireState.Light)
            {
                ParticleSystem fire = Instantiate(firePartical, contact.point, Quaternion.Euler(270,0,0));
                fire.Play();
                fire.transform.parent = gameObject.transform;
                fire.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}