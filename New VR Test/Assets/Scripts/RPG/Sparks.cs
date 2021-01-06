using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparks : MonoBehaviour
{
    [SerializeField] private ParticleSystem partical;
    [SerializeField] private float neededVelocity;

    private void OnCollisionEnter(Collision collision)
    {
            foreach (ContactPoint contact in collision.contacts)
            {
            if (contact.thisCollider.gameObject.tag == "Blade" && gameObject.GetComponent<Rigidbody>().velocity.magnitude >= neededVelocity)
            {
                Instantiate(partical, contact.point, Quaternion.identity);
            }
        }
    }
}