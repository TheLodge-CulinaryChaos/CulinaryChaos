using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTarget;

    // how fast the camera will change its pos and rot
    public float pLerp = 0.02f;
    public float rLerp = 0.01f;

    // make it public for everyone to try
    public int camPosX = 0;
    public int camPosY = 15;
    public int camPosZ = -15;

    public int camRotX = 50;
    public int camRotY = 0;
    public int camRotZ = 0;

    public int camFOV = 50;

    // Update is called once per frame
    void Update()
    {
        // camera position
        Vector3 desiredPosition = camTarget.position + new Vector3(camPosX, camPosY, camPosZ);
        // camera rotation
        Quaternion desiredRotation = Quaternion.Euler(camRotX, camRotY, camRotZ);
        // camera FOV
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, camFOV, rLerp);

        // make the camera follow the character
        transform.position = Vector3.Lerp(transform.position, desiredPosition, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, rLerp);
    }
}
