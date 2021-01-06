using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class IKHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> bones;
    [SerializeField] private List<Transform> knee;

    void LateUpdate()
    {
        Vector3 direction;

        for (int i = 0; i < bones.Count; i++)
        {
            if (i < knee.Count)
            {
                direction = knee[i].position - bones[i].position;
                bones[i].LookAt(knee[i], direction);
            }
            else
            {
                direction = target.position - bones[i].position;
                bones[i].LookAt(target, direction);
            }

        }
    }
}