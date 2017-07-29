// Date   : 29.07.2017 11:53
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class WorldItem : MonoBehaviour {

    [SerializeField]
    private SpriteOutline spriteOutline;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private BoxCollider2D boxCollider2D;

    [SerializeField]
    [Range(2, 10)]
    private int minSize = 4;
    public int MinSize { get { return minSize; } }

    // TODO TEXT

    public void Init(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        boxCollider2D.enabled = false;
    }

    public void Place(Vector3 position)
    {
        transform.localPosition = position;
        boxCollider2D.enabled = true;
    }

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