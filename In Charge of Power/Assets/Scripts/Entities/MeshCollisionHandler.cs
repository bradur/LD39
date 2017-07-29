// Date   : 29.07.2017 18:26
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class MeshCollisionHandler : MonoBehaviour
{

    [SerializeField]
    private Color highlightColor;

    private Color originalColor;

    private MeshRenderer meshRenderer;

    private bool allow = false;

    private int size = 0;
    public int Size { get { return size; } }

    private float highestX;
    public float HighestX { get { return highestX; } }

    private float highestY;

    public float HighestY { get { return highestY; } }


    void Start()
    {
    }

    void Update()
    {

    }

    public void Init(int size, float highestX, float highestY)
    {
        this.size = size;
        this.highestX = highestX;
        this.highestY = highestY;
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
        allow = true;
    }

    private void OnMouseEnter()
    {
        if (allow)
        {
            DebugLogger.Log("You entered!");
            WorldItem item = PlacementManager.main.GetSelectedItem();
            if (item != null && item.MinSize <= size)
            {
                meshRenderer.material.color = highlightColor;
            }
        }
    }

    private void OnMouseUp()
    {
        if (allow)
        {
            DebugLogger.Log("You clicked!");
            PlacementManager.main.PlaceItem(PlacementManager.main.GetSelectedItem(), this);
        }
    }

    private void OnMouseExit()
    {
        if (allow)
        {
            DebugLogger.Log("You exited!");
            meshRenderer.material.color = originalColor;
        }
    }
}
