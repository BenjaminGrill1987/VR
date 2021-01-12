using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour
{
    [SerializeField] private int chainsLength;

    [SerializeField] private Transform target;
    [SerializeField] private Transform pole;
    private Transform[] bones;
    private Vector3[] positions;
    private Vector3[] startDirectionSucc;
    private Quaternion[] startRotationBone;
    private Quaternion startRotationTarget;
    private Quaternion startRotationRoot;

    [SerializeField] private int iterations;

    [SerializeField] private float delta;

    [SerializeField] [Range(0, 1)] float snapBackStrength;

    private float[] bonesLength;

    private float completeLength;

    [SerializeField] bool setInit = false; 

    private void Awake()
    {
        Init();
    }

    void LateUpdate()
    {
        ResolveIK();
    }

    private void ResolveIK()
    {

        if (target == null)
        {
            return;
        }

        if (bonesLength.Length != chainsLength || setInit)
        {
            setInit = false;
            Init();
        }

        for (int i = 0; i < bones.Length; i++)
        {
            positions[i] = bones[i].position;
        }

        var rootRot = (bones[0].parent != null) ? bones[0].parent.rotation : Quaternion.identity;
        var rootRotDifference = rootRot * Quaternion.Inverse(startRotationRoot);

        if (Vector3.Distance(target.position, bones[0].position) >= completeLength)
        {
            var direction = (target.position - positions[0]).normalized;
            for (int i = 1; i < positions.Length; i++)
            {
                positions[i] = positions[i - 1] + direction * bonesLength[i - 1];
            }
        }
        else
        {
            for (int i = 0; i < positions.Length - 1; i++)
            {
                positions[i + 1] = Vector3.Lerp(positions[i + 1], positions[i] + rootRotDifference * startDirectionSucc[i], snapBackStrength);
            }

            for (int i = 0; i < iterations; i++)
            {
                for (int j = positions.Length - 1; j > 0; j--)
                {
                    if (j == positions.Length - 1)
                    {
                        positions[j] = target.position;
                    }
                    else
                    {
                        positions[j] = positions[j + 1] + (positions[j] - positions[j + 1]).normalized * bonesLength[j];
                    }
                }

                for (int j = 1; j < positions.Length; j++)
                {
                    positions[j] = positions[j - 1] + (positions[j] - positions[j - 1]).normalized * bonesLength[j - 1];
                }

                if (Vector3.Distance(target.position, positions[positions.Length - 1]) < delta)
                {
                    break;
                }

            }
        }

        if (pole != null)
        {
            for (int i = 1; i < positions.Length - 1; i++)
            {
                var plane = new Plane(positions[i + 1] - positions[i - 1], positions[i - 1]);
                var projectedPole = plane.ClosestPointOnPlane(pole.position);
                var projectedBone = plane.ClosestPointOnPlane(positions[i]);
                var angle = Vector3.SignedAngle(projectedBone - positions[i - 1], projectedPole - positions[i - 1], plane.normal);
                positions[i] = Quaternion.AngleAxis(angle, plane.normal) * (positions[i] - positions[i - 1]) + positions[i - 1];
            }
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (i == positions.Length - 1)
            {
                bones[i].rotation = target.rotation * Quaternion.Inverse(startRotationTarget) * startRotationBone[i];
            }
            else
            {
                bones[i].rotation = Quaternion.FromToRotation(startDirectionSucc[i], positions[i + 1] - positions[i]) * startRotationBone[i];
            }
            bones[i].position = positions[i];
        }

    }

    private void Init()
    {
        bones = new Transform[chainsLength + 1];
        positions = new Vector3[chainsLength + 1];
        bonesLength = new float[chainsLength];
        startDirectionSucc = new Vector3[chainsLength + 1];
        startRotationBone = new Quaternion[chainsLength + 1];

        completeLength = 0;

        startRotationTarget = target.rotation;

        Transform current = transform;
        for (int i = bones.Length - 1; i >= 0; i--)
        {
            bones[i] = current;
            startRotationBone[i] = current.rotation;

            if (i == bones.Length - 1)
            {
                startDirectionSucc[i] = target.position - current.position;
            }
            else
            {
                startDirectionSucc[i] = bones[i + 1].position - current.position;
                bonesLength[i] = startDirectionSucc[i].magnitude;
                completeLength += bonesLength[i];
            }

            current = current.parent;
        }
    }

}