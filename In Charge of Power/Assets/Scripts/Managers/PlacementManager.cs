// Date   : 29.07.2017 17:36
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlacementManager : MonoBehaviour
{

    public static PlacementManager main;

    private WorldItem selectedItem;
    private ShopItem displayItem;
    private RectTransform displayItemRT;

    private bool isPlacing = false;
    public bool IsPlacing { get { return isPlacing; } }

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
        
        if (item.MinSize <= placementTarget.Size)
        {
            selectedItem.Place(new Vector3(placementTarget.LowestX, placementTarget.LowestY, 0f), placementTarget);
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

    public WorldItem GetSelectedItem()
    {
        return selectedItem;
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
