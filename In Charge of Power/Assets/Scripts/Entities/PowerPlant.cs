// Date   : 29.07.2017 11:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class PowerPlant : MonoBehaviour {

    [SerializeField]
    private SpriteOutline spriteOutline;

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
        spriteOutline.EnableOutline();
    }

    private void OnMouseExit()
    {
        CursorManager.main.SetCursor(CursorType.Default);
        spriteOutline.DisableOutline();
    }

    private void OnMouseUp()
    {

    }



}
