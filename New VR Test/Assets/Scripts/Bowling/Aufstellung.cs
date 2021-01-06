using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Aufstellung : MonoBehaviour
{
    [SerializeField] List<Transform> pinPosition;
    [SerializeField] GameObject pin;
    [SerializeField] TextMeshPro text;
    [SerializeField] BowlingFrameController frameController;
    [SerializeField] PointsCounter pointsCounter;
     
    private List<GameObject> pins;

    private int points = 0;

    public int Points { get => points;}

    void Start()
    {
        pins = new List<GameObject>();
        text.text = points.ToString();
    }

    public void BuildUpPins()
    {
        for (int i = 0; i < pinPosition.Count; i++)
        {
            GameObject bowlingPin = Instantiate(pin, pinPosition[i].position, Quaternion.identity);
            bowlingPin.GetComponent<Pin>().SetOptions(this);
            pins.Add(bowlingPin);
        }
    }

    public void NewPins()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            Destroy(pins[i]);
        }
        pins.Clear();
        StartCoroutine("WaitForMe");
    }

    public void ClearPins()
    {
        List<GameObject> copyPins = new List<GameObject>(pins);

        for (int i = 0; i < pins.Count; i++)
        {
            if(!pins[i].GetComponent<Pin>().NotFallen)
            {
                GameObject bowlPin = pins[i];
                copyPins.Remove(pins[i]);
                Destroy(bowlPin);
            }
        }
        pins = copyPins;
    }

    public void DeletePin()
    {
            points++;
            text.text = points.ToString();
            pointsCounter.AddPoints(1);
    }

    public void ClearPoints()
    {
        points = 0;
        text.text = points.ToString();
    }

    private IEnumerator WaitForMe()
    {
        yield return new WaitForSeconds(3);
        BuildUpPins();
    }
}