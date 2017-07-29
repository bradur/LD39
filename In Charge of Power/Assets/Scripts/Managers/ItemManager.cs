// Date   : 29.07.2017 17:34
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ItemType
{
    None,
    CoalPlantOne,
    CoalPlantTwo,
    CoalPlantThree,
    CoalPlantFour,
    WaterTurbineOne,
    WaterTurbineTwo,
    WindMillOne,
    WindMillTwo,
    NuclearPlant
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("ItemManager").Length == 0)
        {
            main = this;
            gameObject.tag = "ItemManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<GameItem> items = new List<GameItem>();

    public GameItem GetItem(ItemType itemType)
    {
        for (int i = 0; i < items.Count; i += 1)
        {
            if (items[i].itemType == itemType)
            {
                return items[i];
            }
        }
        return null;
    }

    public ResourceItem GetInputResource(ItemType itemType)
    {
        return ResourceManager.main.GetResource(GetItem(itemType).inputType);
    }

    public ResourceItem GetOutputResource(ItemType itemType)
    {
        return ResourceManager.main.GetResource(GetItem(itemType).outputType);
    }

    void Start()
    {

    }

    void Update()
    {

    }
}

[System.Serializable]
public class GameItem : System.Object
{
    public ItemType itemType;
    public ResourceType inputType;
    public ResourceType outputType;
    public Sprite sprite;
    public WorldItem prefab;
}
