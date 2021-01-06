using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEndScript : MonoBehaviour
{
    [SerializeField] private GameObject bowlingBall;
    [SerializeField] private Transform output;
    [SerializeField] private BowlingFrameController frameController;
    [SerializeField] private Aufstellung aufstellung;

    private Timer timer;

    private bool timerStart = false;

    private void Start()
    {
        SpawnSphere();
        timer = new Timer();
        timer.SetTime(3);
    }

    private void FixedUpdate()
    {
        if(timerStart)
        {
            timer.Tick();
        }

        if (timer.CurrentTime <= 0)
        {
            timerStart = false;
            timer.ResetTime();
            if(frameController.CurrentFrame == FrameState.FirstFrame)
            {
                StartCoroutine("ClearThePins");
            }
            ResetBall();
            ChangeFrame();
        }
    }

    private void ResetBall()
    {
        SpawnSphere();
    }

    private void ChangeFrame()
    {
        if (frameController.CurrentFrame != FrameState.WarmUp)
        {
            frameController.ChangeFrame();
        }
    }

    private IEnumerator ClearThePins()
    {
        yield return new WaitForSeconds(3);
        aufstellung.ClearPins();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BowlingKugel")
        {
            Destroy(other.gameObject);
            timerStart = true;
        }
    }

    private void SpawnSphere()
    {
        Instantiate(bowlingBall, output.position, Quaternion.identity);
    }
}