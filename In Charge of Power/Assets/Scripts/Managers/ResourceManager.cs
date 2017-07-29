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
    Power
}

public class ResourceManager : MonoBehaviour {

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

}

[System.Serializable]
public class ResourceItem : System.Object
{
    public ResourceType resourceType;
    public Sprite sprite;
}