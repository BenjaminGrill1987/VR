using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    [SerializeField] private Transform cameraMain;
    [SerializeField] private float zOffset;
    [SerializeField] private float angletoTurn;

    private void LateUpdate()
    {
        float angle;

        angle = cameraMain.localEulerAngles.y - gameObject.transform.localEulerAngles.y;

        Debug.LogError(angle.ToString());
        gameObject.transform.position = new Vector3(cameraMain.position.x, gameObject.transform.position.y, cameraMain.position.z + zOffset);

        if(angle > angletoTurn || angle < - angletoTurn)
        {
            Quaternion oldAngle = gameObject.transform.localRotation;
            Quaternion newAngle = Quaternion.Euler(gameObject.transform.localRotation.x, cameraMain.localRotation.y - angletoTurn, gameObject.transform.localRotation.z);

            gameObject.transform.rotation = Quaternion.Slerp(oldAngle,newAngle,1);

        }
        
        
    }
}