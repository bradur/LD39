// Date   : 29.07.2017 17:36
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlacementManager : MonoBehaviour
{

    public static PlacementManager main;

    private WorldItem selectedItem;
    private ShopItem displayItem;
    private RectTransform displayItemRT;

    private bool isPlacing = false;
    public bool IsPlacing { get { return isPlacing; } }

    private List<WorldItem> placedItems = new List<WorldItem>();

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("PlacementManager").Length == 0)
        {
            main = this;
            gameObject.tag = "PlacementManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }


    [SerializeField]
    private Transform placementContainer;

    [SerializeField]
    private Transform displayContainer;

    void Start()
    {

    }

    public bool AllowPlacement(LayerType layerType)
    {
        return (displayItem != null && displayItem.RequiredLayer == layerType);
    }

    public bool PlaceItem(WorldItem item, MeshCollisionHandler placementTarget)
    {
        if (item == null)
        {
            return false;
        }
        if (!AllowPlacement(placementTarget.LayerType))
        {
            return false;
        }
        if (item.MinSize <= placementTarget.Size)
        {
            selectedItem.Place(new Vector3(placementTarget.LowestX, placementTarget.LowestY, 0f), placementTarget);
            placedItems.Add(selectedItem);
            PowerManager.main.RecalculateRate();
            MoneyManager.main.RecalculateRate();
            isPlacing = false;
            selectedItem = null;
            displayItem.Kill();
            displayItem = null;
            return true;
        }
        else
        {
            SoundManager.main.PlaySound(SoundType.CantPlaceThere);
            return false;
        }
    }

    public void RemovePlacedItem(WorldItem item)
    {
        placedItems.Remove(item);
        PowerManager.main.RecalculateRate();
        MoneyManager.main.RecalculateRate();
    }

    void Update()
    {
        if (selectedItem != null)
        {
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mousePosition = Input.mousePosition;

            displayItem.transform.position = new Vector3(
                mousePosition.x + displayItemRT.sizeDelta.x / 3,
                mousePosition.y + displayItemRT.sizeDelta.y / 4,
                displayItem.transform.position.z
            );
        }
    }

    public void DisplaySellBack()
    {
        if (isPlacing)
        {
            CursorManager.main.SetCursor(CursorType.Pointer);
            UIManager.main.ShowMouseMessage(string.Format(
                "Sell {0} back for full price (${1}) before placing it.", displayItem.ItemName, displayItem.Cost
            ), true);
        }
    }

    public float GetRate(ResourceType resourceType)
    {
        float rate = 0f;
        for (int i = 0; i < placedItems.Count; i += 1)
        {
            if (placedItems[i].GameItem.outputType == resourceType && placedItems[i].CanProduce)
            {
                rate += placedItems[i].GetOutputRate();
            }
            else if (placedItems[i].GameItem.inputType == resourceType && placedItems[i].CanProduce)
            {
                rate += placedItems[i].GetInputRate();
            }
        }
        return rate;
    }

    public WorldItem GetSelectedItem()
    {
        return selectedItem;
    }

    public void UnselectItem()
    {
        if (selectedItem != null)
        {
            MoneyManager.main.Topup(displayItem.Cost);
            selectedItem.Kill();
            displayItem.Kill();
            isPlacing = false;
            displayItem = null;
            selectedItem = null;
        }
    }

    public void SelectItem(ShopItem shopItem, int inputCount, int outputCount)
    {
        isPlacing = true;
        GameItem item = ItemManager.main.GetItem(shopItem.ItemType);
        // TODO CHECK FOR CURRENTLY SELECTED ITEM
        selectedItem = Instantiate(item.prefab);
        selectedItem.transform.SetParent(placementContainer, false);
        selectedItem.Init(item, inputCount, outputCount, shopItem.Cost, shopItem.ItemName);

        displayItem = Instantiate(shopItem);
        displayItemRT = displayItem.GetComponent<RectTransform>();
        displayItem.DisableShopCapabilities();
        displayItem.transform.SetParent(displayContainer, false);
    }
}
