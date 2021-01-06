using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugSword : MonoBehaviour
{
    [SerializeField] private Text text1, text2;
    [SerializeField] private Rigidbody rigidbody;

    private float currentVelocity, maxVelocity;

    private void Update()
    {
        currentVelocity = rigidbody.velocity.magnitude;

        if(currentVelocity > maxVelocity)
        {
            maxVelocity = currentVelocity;
        }

        text1.text = "Current velocity: " + currentVelocity.ToString();
        text2.text = "Max velocity: " + maxVelocity.ToString();
    }
}