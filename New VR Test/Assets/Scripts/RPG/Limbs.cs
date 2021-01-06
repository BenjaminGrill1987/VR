using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limbs : MonoBehaviour
{
    [SerializeField] private ConfigurableJoint cj;
    [SerializeField] private float neededVelocity;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Blade")
        {
            if (other.gameObject.transform.parent.GetComponent<Rigidbody>().velocity.magnitude >= neededVelocity)
            {
                if (cj != null)
                {
                    Destroy(cj);
                }
            }
        }
    }
}