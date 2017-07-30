// Date   : 30.07.2017 08:29
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    [Range(0f, 3f)]
    private float marginVertical;

    [SerializeField]
    [Range(0f, 3f)]
    private float marginHorizontal;

    [SerializeField]
    private List<ShopItem> shopItems = new List<ShopItem>();

    [SerializeField]
    private Transform container;

    private void Start()
    {
        float currentY = -marginVertical;
        for (int i = 0; i < shopItems.Count; i += 1)
        {
            ShopItem shopItem = Instantiate(shopItems[i]);
            shopItem.Init();
            RectTransform rectTransform = shopItem.GetComponent<RectTransform>();
            rectTransform.SetParent(container, false);
            rectTransform.anchoredPosition = new Vector2(
                rectTransform.anchoredPosition.x,
                currentY
            );
            currentY -= rectTransform.sizeDelta.y + marginVertical;
        }
    }
}
