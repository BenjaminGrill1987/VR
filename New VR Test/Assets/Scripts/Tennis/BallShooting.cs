using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallShooting : MonoBehaviour
{
    [SerializeField] private Transform directionTransform;
    [SerializeField] private Transform ballSpawn;
    [SerializeField] private float minVelocity, maxVelocity;
    [SerializeField] private GameObject ball;
    [SerializeField] private float timeBetweenShoot;
    [SerializeField] private TextMeshPro text;

    private Vector3 direction;
    private bool startShooting = false;
    private Timer timer;

    private void Start()
    {
        text.text = "Shooting = " + startShooting;
        direction = directionTransform.position - ballSpawn.position;
        timer = new Timer();
        timer.SetTime(timeBetweenShoot);
    }

    private void FixedUpdate()
    {
        if (startShooting)
        {
            if (timer.CurrentTime <= 0)
            {
                BallShoot();
                timer.ResetTime();
            }
            else
            {
                timer.Tick();
            }
        }
    }

    private void BallShoot()
    {
        float ballspeed = Random.Range(minVelocity, maxVelocity);
        GameObject ballCopy = Instantiate(ball, ballSpawn.position, Quaternion.identity);
        ballCopy.GetComponent<Rigidbody>().velocity = direction * ballspeed;
    }

    public void SetStartBool()
    {
        startShooting = !startShooting;
        text.text = "Shooting = " + startShooting;
    }
}