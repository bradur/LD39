// Date   : 29.07.2017 13:14
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class CameraZoomManager : MonoBehaviour
{

    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    [Range(5f, 6f)]
    float minZoomLevel = 5f;

    [SerializeField]
    [Range(6f, 20f)]
    float maxZoomLevel = 6f;

    [SerializeField]
    [Range(5f, 20f)]
    float zoomLevel = 5f;

    [SerializeField]
    [Range(0.05f, 1f)]
    float zoomStep = 0.05f;

    void Start()
    {

    }

    void Update()
    {
        if (!GameManager.main.GameIsOver)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                // scroll up
                zoomLevel -= zoomStep;
            }
            else if (scroll < 0f)
            {
                // scroll down
                zoomLevel += zoomStep;
            }
            zoomLevel = Mathf.Clamp(zoomLevel, minZoomLevel, maxZoomLevel);
            targetCamera.orthographicSize = zoomLevel;
        }
    }
}
