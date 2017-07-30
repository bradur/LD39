// Date   : 29.07.2017 13:28
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class CameraPanManager : MonoBehaviour
{

    [SerializeField]
    [Range(0.5f, 5f)]
    private float mouseMoveBoundary = 1f;

    private float screenHeight;
    private float screenWidth;

    [SerializeField]
    [Range(0.01f, 0.5f)]
    private float speed;

    [SerializeField]
    [Range(0.2f, 1f)]
    private float maxSpeed = 0.3f;

    [SerializeField]
    [Range(3f, 10f)]
    private float cameraMaxX;

    [SerializeField]
    [Range(-8f, 10f)]
    private float cameraMinX;

    [SerializeField]
    [Range(-5f, 20f)]
    private float cameraMaxY;

    [SerializeField]
    [Range(-20f, 10f)]
    private float cameraMinY;

    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private bool panningEnabled = false;



    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    void Update()
    {
        if (!GameManager.main.GameIsOver)
        {
            if (Input.mousePosition.x > (screenWidth - mouseMoveBoundary))
            {
                MoveCamera(1, 0, Input.mousePosition.x - (screenWidth - mouseMoveBoundary));
            }
            if (Input.mousePosition.x < mouseMoveBoundary)
            {
                MoveCamera(-1, 0, Mathf.Abs(Input.mousePosition.x - mouseMoveBoundary));
            }
            if (Input.mousePosition.y > (screenHeight - mouseMoveBoundary))
            {
                MoveCamera(0, 1, Input.mousePosition.y - (screenHeight - mouseMoveBoundary));
            }
            if (Input.mousePosition.y < mouseMoveBoundary)
            {
                MoveCamera(0, -1, Mathf.Abs(Input.mousePosition.y - mouseMoveBoundary));
            }
        }
    }

    void MoveCamera(float xDir, float yDir, float diff)
    {
        float xSpeed = xDir * Mathf.Clamp(speed * diff, speed, maxSpeed);
        float ySpeed = yDir * Mathf.Clamp(speed * diff, speed, maxSpeed);
        if (panningEnabled) {
            float newX = Mathf.Clamp(targetCamera.transform.position.x + xSpeed, cameraMinX, cameraMaxX);
            float newY = Mathf.Clamp(targetCamera.transform.position.y + ySpeed, cameraMinY, cameraMaxY);
            targetCamera.transform.position = new Vector3(
                newX,
                newY,
                targetCamera.transform.position.z
            );
        }
    }
}
