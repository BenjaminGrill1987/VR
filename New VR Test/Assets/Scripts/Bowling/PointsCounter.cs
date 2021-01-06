using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    [SerializeField] private BowlingFrameController frameController;

    private Dictionary<string, int> playerPoints;

    int index = 0;

    public void GetPlayerNumber(int newPlayer)
    {
        playerPoints = new Dictionary<string, int>();
        for (int i = 0; i < newPlayer; i++)
        {
            playerPoints.Add("Player " + (i + 1).ToString(), 0);
        }
    }

    public void ShowPlayerAndPoints()
    {
        int i = 0;
        foreach (var key in playerPoints)
        {
            if (i == index)
            {
                text.text = key.Key + ": " + key.Value.ToString();
            }
            i++;
        }
    }

    public void AddPoints(int newPoints)
    {
        int i = 0;
        foreach (var key in playerPoints)
        {
            if (i == index)
            {
                string keyName = key.Key;
                playerPoints[keyName] += newPoints;
                break;
            }
            i++;
        }
        ShowPlayerAndPoints();
    }

    public void AddIndex()
    {
        index++;
        if (index >= playerPoints.Count)
        {
            index = 0;
        }
    }
}