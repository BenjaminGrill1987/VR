using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRegister : MonoBehaviour
{
    [SerializeField] private Aufstellung aufstellung;
    [SerializeField] private PointsCounter pointsCounter;
    [SerializeField] private BowlingFrameController frameController;

    public void OnePlayer()
    {
        pointsCounter.GetPlayerNumber(1);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }

    public void TwoPlayer()
    {
        pointsCounter.GetPlayerNumber(2);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }

    public void ThreePlayer()
    {
        pointsCounter.GetPlayerNumber(3);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }

    public void FourPlayer()
    {
        pointsCounter.GetPlayerNumber(4);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }

    public void FivePlayer()
    {
        pointsCounter.GetPlayerNumber(5);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }

    public void SixPlayer()
    {
        pointsCounter.GetPlayerNumber(6);
        pointsCounter.ShowPlayerAndPoints();
        aufstellung.BuildUpPins();
        frameController.ChangeFrame();
    }
}