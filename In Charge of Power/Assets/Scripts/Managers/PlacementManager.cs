// Date   : 29.07.2017 17:36
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlacementManager : MonoBehaviour
{

    public static PlacementManager main;

    private WorldItem selectedItem;

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

    void Start()
    {

    }

    public void PlaceItem(WorldItem item, MeshCollisionHandler placementTarget)
    {
        if (item.MinSize <= placementTarget.Size)
        {
            selectedItem.Place(new Vector3(placementTarget.HighestX, placementTarget.HighestY, 0f));
            selectedItem = null;
        }
        else
        {
            SoundManager.main.PlaySound(SoundType.CantPlaceThere);
        }
    }

    void Update()
    {
        if (selectedItem != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedItem.transform.position = new Vector3(
                mousePosition.x,
                mousePosition.y,
                selectedItem.transform.position.z
            );
        }
    }

    public WorldItem GetSelectedItem()
    {
        return selectedItem;
    }

    public void SelectItem(GameItem item)
    {
        // TODO CHECK FOR CURRENTLY SELECTED ITEM
        selectedItem = Instantiate(item.prefab);
        selectedItem.transform.SetParent(placementContainer, false);
        item.prefab.Init(item.sprite);
    }
}