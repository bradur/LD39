// Date   : 31.07.2017 00:54
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class MousePositionManager : MonoBehaviour {

    public static MousePositionManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MousePositionManager").Length == 0)
        {
            main = this;
            gameObject.tag = "MousePositionManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float refWidth = 1280f;
    private float refHeight = 720f;

    public Vector2 GetMultiplier()
    {
        return new Vector2(Screen.width / refWidth, Screen.height / refHeight);
    }

    public Vector2 GetNormalizedAnything(Vector2 normalizeThis)
    {
        return new Vector2(refWidth / Screen.width * normalizeThis.x, refHeight / Screen.height * normalizeThis.y);
    }

    public Vector2 GetNormalizedMousePosition ()
    {
        return new Vector2(refWidth / Screen.width * Input.mousePosition.x, refHeight / Screen.height * Input.mousePosition.y);
    }
}
