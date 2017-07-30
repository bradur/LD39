// Date   : 29.07.2017 18:26
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

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

    private float lowestX;
    public float LowestX { get { return lowestX; } }

    private float lowestY;

    public float LowestY { get { return lowestY; } }

    private LayerType layerType;
    public LayerType LayerType { get { return layerType; } }

    private bool occupied = false;

    void Start()
    {
    }

    void Update()
    {

    }

    public void Init(int size, float lowestX, float lowestY, LayerType layerType)
    {
        this.size = size;
        this.lowestX = lowestX;
        this.lowestY = lowestY;
        this.layerType = layerType;
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
        allow = true;
    }

    private void OnMouseEnter()
    {
        if (allow && !occupied && !EventSystem.current.IsPointerOverGameObject() && !GameManager.main.GameIsOver)
        {
            if (PlacementManager.main.IsPlacing)
            {
                if (PlacementManager.main.AllowPlacement(layerType))
                {
                    WorldItem item = PlacementManager.main.GetSelectedItem();
                    if (item != null && item.MinSize <= size)
                    {
                        meshRenderer.material.color = highlightColor;
                        CursorManager.main.SetCursor(CursorType.Pointer);

                    }
                } else
                {
                    UIManager.main.ShowMouseMessage(string.Format("You can't place this item here!", layerType, ShopManager.main.GetItem(layerType).ItemName));
                }
            }
            else
            {
                UIManager.main.ShowMouseMessage(string.Format("This is a {0}. You can place a {1} here.", layerType, ShopManager.main.GetItem(layerType).ItemName));
            }
        }
    }

    private void OnMouseUp()
    {
        if (allow && !occupied && !EventSystem.current.IsPointerOverGameObject() && !GameManager.main.GameIsOver)
        {
            if (PlacementManager.main.PlaceItem(PlacementManager.main.GetSelectedItem(), this))
            {
                meshRenderer.material.color = originalColor;
                occupied = true;
            }
            else
            {
                // TODO unallowed placement
            }
            UIManager.main.ClearStaticMessage();
        }
    }

    public void Clear()
    {
        occupied = false;
    }

    private void OnMouseExit()
    {
        if (allow)
        {
            meshRenderer.material.color = originalColor;
            CursorManager.main.SetCursor(CursorType.Default);
            UIManager.main.ClearStaticMessage();
        }
    }
}
