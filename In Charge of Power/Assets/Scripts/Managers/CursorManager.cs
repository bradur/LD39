// Date   : 29.07.2017 11:46
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public enum CursorType
{
    None,
    Default,
    Pointer
}

public class CursorManager : MonoBehaviour {

    [SerializeField]
    private Texture2D cursorDefault;

    [SerializeField]
    private Vector2 cursorDefaultHotSpot;

    [SerializeField]
    private Texture2D cursorPointer;

    [SerializeField]
    private Vector2 cursorPointerHotSpot;

    public static CursorManager main;

    void Awake ()
    {
        if (GameObject.FindGameObjectsWithTag("CursorManager").Length == 0)
        {
            main = this;
            gameObject.tag = "CursorManager";
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SetCursor (CursorType cursor)
    {
        if (cursor == CursorType.Default)
        {
            Cursor.SetCursor(cursorDefault, cursorDefaultHotSpot, CursorMode.Auto);
        } else if (cursor == CursorType.Pointer)
        {
            Cursor.SetCursor(cursorPointer, cursorPointerHotSpot, CursorMode.Auto);
        }
    }
}
