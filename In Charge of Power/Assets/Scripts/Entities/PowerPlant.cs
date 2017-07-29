// Date   : 29.07.2017 11:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class PowerPlant : MonoBehaviour {

    void Start () {
    
    }

    void Update () {
    
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseEnter()
    {
        CursorManager.main.SetCursor(CursorType.Pointer);
    }

    private void OnMouseExit()
    {
        CursorManager.main.SetCursor(CursorType.Default);
    }

    private void OnMouseUp()
    {

    }



}
