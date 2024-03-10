using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public float mouseSensitivity = 100f;
    public float zoomSensitivity = 10f;
    public float minFOV = 15f;
    public float maxFOV = 90f;

    private Transform ballTransform;
    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        ballTransform = GameObject.Find("Player").transform;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;

        // Apply the rotation for looking up and down, and left and right
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

        // Zoom in/out with mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0)
        {
            mainCamera.fieldOfView -= scroll * zoomSensitivity;
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, minFOV, maxFOV);
        }
    }
}
