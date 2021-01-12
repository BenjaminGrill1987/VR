using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    [SerializeField] List<Transform> nextTarget;
    [SerializeField] List<Transform> currentTarget;

    [SerializeField] float walkingDistance;

    [SerializeField] float movementSpeed;

    private float average;

    private Vector3 rootTransform;

    private void Start()
    {
        rootTransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        BodyOffSet();
        BodyAngle();
    }

    private void Movement()
    {
        average = 0;

        transform.position += new Vector3(0, 0, Time.deltaTime * movementSpeed);

        for (int i = 0; i < nextTarget.Count; i++)
        {
            float distance;
            Vector3 raycastPosition = new Vector3(nextTarget[i].position.x, nextTarget[i].position.y + 0.5f, nextTarget[i].position.z);
            Ray ray = new Ray(raycastPosition, Vector3.down);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                nextTarget[i].position = new Vector3(nextTarget[i].position.x, hitInfo.point.y, nextTarget[i].position.z);
            }
            distance = Vector3.Distance(nextTarget[i].position, currentTarget[i].position);
            Debug.DrawLine(nextTarget[i].position, currentTarget[i].position, Color.red);

            if (distance > walkingDistance)
            {
                isGrounded(i);
            }
        }
    }

    private void BodyOffSet()
    {
        for (int i = 0; i < currentTarget.Count; i++)
        {
            average += currentTarget[i].position.y;
        }

        average = average / currentTarget.Count;

        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, rootTransform.y + average, transform.position.z), 0.5f);
    }

    private void BodyAngle()
    {
        float differenceFront = currentTarget[0].position.y - currentTarget[2].position.y;
        float differenceBack = currentTarget[1].position.y - currentTarget[3].position.y;
        float distanceFront = Vector3.Distance(currentTarget[0].position, currentTarget[2].position);
        float distanceBack = Vector3.Distance(currentTarget[1].position, currentTarget[3].position);

        float angleFront = Mathf.Rad2Deg * Mathf.Atan(differenceFront / distanceFront);
        float angleBack = Mathf.Rad2Deg * Mathf.Atan(differenceBack / distanceBack);

        float bodyAngle = (angleFront + angleBack) / (currentTarget.Count / 2);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + bodyAngle), 0.5f);
    }

    private void isGrounded(int leg)
    {
        if (leg == 0 || leg == 2)
        {
            if (Physics.Raycast(new Vector3(currentTarget[1].position.x, currentTarget[1].position.y + 0.1f, currentTarget[1].position.z), Vector3.down, 0.2f) &&
                Physics.Raycast(new Vector3(currentTarget[3].position.x, currentTarget[3].position.y + 0.1f, currentTarget[3].position.z), Vector3.down, 0.2f))
            {
                StartCoroutine(Walking(currentTarget[0], nextTarget[0].position, currentTarget[2], nextTarget[2].position));
            }
        }
        else
        {
            if (Physics.Raycast(new Vector3(currentTarget[0].position.x, currentTarget[0].position.y + 0.1f, currentTarget[0].position.z), Vector3.down, 0.2f) &&
                Physics.Raycast(new Vector3(currentTarget[2].position.x, currentTarget[2].position.y + 0.1f, currentTarget[2].position.z), Vector3.down, 0.2f))
            {
                StartCoroutine(Walking(currentTarget[1], nextTarget[1].position, currentTarget[3], nextTarget[3].position));
            }
        }
    }

    IEnumerator Walking(Transform target, Vector3 position, Transform target2, Vector3 position2)
    {
        AnimationCurve xCurve, xCurve2, yCurve, yCurve2, zCurve, zCurve2;

        xCurve = new AnimationCurve();
        yCurve = new AnimationCurve();
        zCurve = new AnimationCurve();

        xCurve2 = new AnimationCurve();
        yCurve2 = new AnimationCurve();
        zCurve2 = new AnimationCurve();

        xCurve.AddKey(0, target.position.x);
        xCurve.AddKey(0.5f, position.x);

        yCurve.AddKey(0, target.position.y);
        yCurve.AddKey(0.25f, position.y + 1);
        yCurve.AddKey(0.5f, position.y);

        zCurve.AddKey(0, target.position.z);
        zCurve.AddKey(0.5f, position.z);

        xCurve2.AddKey(0, target2.position.x);
        xCurve2.AddKey(0.5f, position2.x);

        yCurve2.AddKey(0, target2.position.y);
        yCurve2.AddKey(0.25f, position2.y + 1);
        yCurve2.AddKey(0.5f, position2.y);

        zCurve2.AddKey(0, target2.position.z);
        zCurve2.AddKey(0.5f, position2.z);

        float elapsedTime = 0;
        while (target.position != position)
        {
            elapsedTime += Time.deltaTime;
            target.position = new Vector3(xCurve.Evaluate(elapsedTime), yCurve.Evaluate(elapsedTime), zCurve.Evaluate(elapsedTime));
            target2.position = new Vector3(xCurve2.Evaluate(elapsedTime), yCurve2.Evaluate(elapsedTime), zCurve2.Evaluate(elapsedTime));
            yield return new WaitForEndOfFrame();
        }
    }
}