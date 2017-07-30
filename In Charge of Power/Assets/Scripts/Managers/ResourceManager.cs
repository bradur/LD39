// Date   : 29.07.2017 17:20
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ResourceType
{
    None,
    Coal,
    Wind,
    Water,
    Nuclear,
    Power,
    Money
}

public class ResourceManager : MonoBehaviour
{

    public static ResourceManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("ResourceManager").Length == 0)
        {
            main = this;
            gameObject.tag = "ResourceManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private List<ResourceItem> resources = new List<ResourceItem>();

    private void Start()
    {
        for (int i = 0; i < resources.Count; i += 1)
        {
            if (resources[i].resourceType != ResourceType.Money && resources[i].resourceType != ResourceType.Power)
            {
                UIManager.main.AddResource(resources[i].amount, resources[i].resourceType);
            }
        }
    }

    public ResourceItem GetResource(ResourceType resourceType)
    {
        for (int i = 0; i < resources.Count; i += 1)
        {
            if (resources[i].resourceType == resourceType)
            {
                return resources[i];
            }
        }
        return null;
    }

    public void AddResource(int amount, ResourceType resourceType)
    {
        if(resourceType == ResourceType.None)
        {
            return;
        }
        if (resourceType == ResourceType.Power)
        {
            PowerManager.main.AddPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            MoneyManager.main.Topup(amount);
        }
        else
        {
            GetResource(resourceType).amount += amount;
            UIManager.main.AddResource(amount, resourceType);
        }
    }

    public bool WithdrawResource(int amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.None)
        {
            return true;
        }
        ResourceItem resourceItem = GetResource(resourceType);
        if (resourceType == ResourceType.Power)
        {
            return PowerManager.main.DrainPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            return MoneyManager.main.Withdraw(amount);
        }
        else if ((resourceItem.amount - amount) > 0)
        {
            resourceItem.amount -= amount;
            UIManager.main.WithdrawResource(amount, resourceType);
            return true;
        }
        return false;
    }

}

[System.Serializable]
public class ResourceItem : System.Object
{
    public ResourceType resourceType;
    public Sprite sprite;
    public int amount;
}