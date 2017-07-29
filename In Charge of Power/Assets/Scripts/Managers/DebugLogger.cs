// Date   : 29.07.2017 17:43
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class DebugLogger : MonoBehaviour {

    [SerializeField]
    private static bool allowLogging = true;

    public static void Log(string message)
    {
        if (allowLogging)
        {
            Debug.Log(message);
        }
    }
}
