using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingFrameController : MonoBehaviour
{
    private FrameState currentFrame;

    public FrameState CurrentFrame { get => currentFrame;}

    [SerializeField] private Aufstellung aufstellung;
    [SerializeField] private PointsCounter pointsCounter;

    private void Start()
    {
        currentFrame = FrameState.WarmUp;
    }

    public void ChangeFrame()
    {
        Debug.LogError(currentFrame);

        if (currentFrame == FrameState.FirstFrame)
        {
            Debug.LogError("ChangeToSecond");
            TryToChangeFrame(FrameState.SecondFrame);
        }
        else
        {
            Debug.LogError("ChangeToFirst");
            TryToChangeFrame(FrameState.FirstFrame);
        }
    }

    public void TryToChangeFrame(FrameState newFrame)
    {
        bool permissionGranted = false;

        if(currentFrame != newFrame)
        {
            permissionGranted = true;
        }

        switch(newFrame)
        {
            case FrameState.WarmUp:
                {
                    if(permissionGranted)
                    {
                        currentFrame = newFrame;
                    }
                    break;
                }
            case FrameState.FirstFrame:
                {
                    if (permissionGranted && currentFrame == FrameState.SecondFrame)
                    {
                        currentFrame = newFrame;
                        aufstellung.ClearPoints();
                        aufstellung.NewPins();
                        pointsCounter.AddIndex();
                        pointsCounter.ShowPlayerAndPoints();                        
                    }
                    else if (permissionGranted && currentFrame == FrameState.WarmUp)
                    {
                        currentFrame = newFrame;
                    }
                    break;
                }
            case FrameState.SecondFrame:
                {
                    if (permissionGranted)
                    {
                        currentFrame = newFrame;
                    }
                    break;
                }
            case FrameState.EndGame:
                {
                    if(permissionGranted)
                    {
                        currentFrame = newFrame;
                    }
                    break;
                }
        }
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        GUI.Label(new Rect(100, 100, 100, 100), currentFrame.ToString());
    }
}


public enum FrameState
{
    WarmUp,
    FirstFrame,
    SecondFrame,
    EndGame
}