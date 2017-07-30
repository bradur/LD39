// Date   : 29.07.2017 13:28
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class CameraPanManager : MonoBehaviour
{

    [SerializeField]
    [Range(0.5f, 50f)]
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

    Vector2 multiplier;

    void Start()
    {
        multiplier = MousePositionManager.main.GetMultiplier();
        speed *= multiplier.x;
        maxSpeed *= multiplier.x;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    void Update()
    {
        if (!GameManager.main.GameIsOver)
        {
            //Vector2 mousePos = MousePositionManager.main.GetNormalizedMousePosition();
            Vector2 mousePos = Input.mousePosition;
            if (mousePos.x > (screenWidth - mouseMoveBoundary))
            {
                MoveCamera(1, 0, mousePos.x - (screenWidth - mouseMoveBoundary));
            }
            if (mousePos.x < mouseMoveBoundary)
            {
                MoveCamera(-1, 0, Mathf.Abs(mousePos.x - mouseMoveBoundary));
            }
            if (mousePos.y > (screenHeight - mouseMoveBoundary))
            {
                MoveCamera(0, 1, mousePos.y - (screenHeight - mouseMoveBoundary));
            }
            if (mousePos.y < mouseMoveBoundary)
            {
                MoveCamera(0, -1, Mathf.Abs(mousePos.y - mouseMoveBoundary));
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
