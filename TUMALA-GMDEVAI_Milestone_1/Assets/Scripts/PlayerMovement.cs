using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerCamera;
    public float movementSpeed = 10.0f;
    public float mouseSpeed = 200.0f;
    private float xCameraRotation = 0.0f;
    private float yCameraRotation = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        xCameraRotation -= mouseY;
        yCameraRotation += mouseX;

        xCameraRotation = Mathf.Clamp(xCameraRotation, -90.0f, 90.0f);

        playerCamera.rotation = Quaternion.Euler(xCameraRotation, yCameraRotation, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, yCameraRotation, 0.0f);


        if ((Input.GetKey(KeyCode.W)))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.A)))
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.S)))
        {
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.D)))
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
    }
}
