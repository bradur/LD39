// Date   : 29.07.2017 13:49
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class SpriteOutline : MonoBehaviour
{

    [SerializeField]
    private Color color;

    [SerializeField]
    [Range(1, 20)]
    private int outlineSize;
    private SpriteRenderer spriteRenderer;

    private bool outlineEnabled = false;

    void Start()
    {
    }

    public void EnableOutline()
    {
        outlineEnabled = true;
    }

    public void DisableOutline()
    {
        UpdateOutline(false);
        outlineEnabled = false;
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateOutline(true);
    }

    private void OnDisable()
    {
        UpdateOutline(false);
    }

    void Update()
    {
        UpdateOutline(true);
    }

    void UpdateOutline(bool outline)
    {
        if (outlineEnabled)
        {
            MaterialPropertyBlock mpb = new MaterialPropertyBlock();
            spriteRenderer.GetPropertyBlock(mpb);
            mpb.SetFloat("_Outline", outline ? 1f : 0);
            mpb.SetColor("_OutlineColor", color);
            mpb.SetFloat("_OutlineSize", outlineSize);
            spriteRenderer.SetPropertyBlock(mpb);
        }
    }
}
