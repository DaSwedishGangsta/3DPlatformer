using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject playerObj;
    float rotationX = 0f;
    float rotationY = 0f;
    public float sensitivity = 15f;

    void Update()
    {
        rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        playerObj.transform.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * -1 * sensitivity, Vector3.up);
        transform.localEulerAngles = new Vector3(rotationY,0, 0);

    }
}
