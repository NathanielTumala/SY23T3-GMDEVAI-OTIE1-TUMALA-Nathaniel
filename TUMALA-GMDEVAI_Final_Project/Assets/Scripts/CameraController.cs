using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSpeed = 5.0f;

    private float cameraVerticalRotation = 0.0f;
    private bool cursorLock = true;

    void Start()
    {
        if (cursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed;

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90.0f, 90.0f);
        transform.localEulerAngles = new Vector3(1.0f, 0.0f, 0.0f) * cameraVerticalRotation;

        player.Rotate(new Vector3(0.0f, 1.0f, 0.0f) * mouseX);
    }
}