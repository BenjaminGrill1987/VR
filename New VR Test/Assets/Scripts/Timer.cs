using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float time, currentTime;

    public float CurrentTime { get => currentTime;}

    public void SetTime(float newTime)
    {
        time = newTime;
        currentTime = newTime;
    }

    public void Tick()
    {
        currentTime -= Time.deltaTime;
    }

    public void ResetTime()
    {
        currentTime = time;
    }
}