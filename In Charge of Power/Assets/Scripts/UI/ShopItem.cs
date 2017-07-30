// Date   : 29.07.2017 16:09
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    private int cost = 0;
    public int Cost { get { return cost; } }

    [SerializeField]
    private string itemName = "item";
    public string ItemName { get { return itemName; } }

    [SerializeField]
    private ItemType itemType = ItemType.None;
    public ItemType ItemType { get { return itemType; } }

    //[SerializeField]
    private KeyCode correspondingKey;

    [SerializeField]
    private ResourceType inputType;

    [SerializeField]
    private LayerType requiredLayer;

    public LayerType RequiredLayer { get { return requiredLayer; } }

    [SerializeField]
    private Image imgItem;

    [SerializeField]
    private Text txtName;

    [SerializeField]
    private Text txtCost;

    [SerializeField]
    private Text txtInputCount;

    [SerializeField]
    private Image imgInput;

    [SerializeField]
    private int inputCount;

    [SerializeField]
    private ResourceType outputType;

    [SerializeField]
    private Image imgOutput;

    [SerializeField]
    private Text txtOutputCount;

    [SerializeField]
    private int outputCount;

    [SerializeField]
    private GameObject bg;

    [SerializeField]
    private GameObject shopInfo;

    public void SetKeyCode(KeyCode key)
    {
        correspondingKey = key;
    }

    public void Init()
    {
        imgInput.sprite = ItemManager.main.GetInputResource(itemType).sprite;
        imgOutput.sprite = ItemManager.main.GetOutputResource(itemType).sprite;
        imgItem.sprite = ItemManager.main.GetItem(itemType).sprite;
        if (inputType != ResourceType.None)
        {
            txtInputCount.text = "" + inputCount;
        }
        else
        {
            imgInput.enabled = false;
        }
        txtOutputCount.text = "" + outputCount;
        txtCost.text = string.Format("$ {0}", cost);
        txtName.text = itemName;
    }

    public void HoverIn()
    {
        if (!GameManager.main.GameIsOver)
        {
            if (!PlacementManager.main.IsPlacing)
            {
                if (MoneyManager.main.GetBalance() >= cost)
                {
                    CursorManager.main.SetCursor(CursorType.Pointer);
                    UIManager.main.ShowMouseMessage(string.Format(
                        "Buy {0} for ${1}", itemName, cost
                    ), true);
                }
                else
                {
                    UIManager.main.ShowMouseMessage(string.Format(
                        "Not enough money (${0})", cost
                    ), true);
                }
            }
            else
            {
                PlacementManager.main.DisplaySellBack();
            }
        }
    }

    public void HoverOut()
    {
        CursorManager.main.SetCursor(CursorType.Default);
        UIManager.main.ClearStaticMessage();
    }

    private void OnMouseEnter()
    {
        HoverIn();
    }

    private void OnMouseExit()
    {
        HoverOut();
    }



    public void DisableShopCapabilities()
    {
        bg.SetActive(false);
        shopInfo.SetActive(false);
        GetComponent<Button>().enabled = false;
        GetComponent<EventTrigger>().enabled = false;
        Image parentImage = GetComponent<Image>();
        if (parentImage != null)
        {
            parentImage.raycastTarget = false;
        }
        foreach (Transform child in transform)
        {
            Image image = GetComponent<Image>();
            if (image != null)
            {
                image.raycastTarget = false;
            }
        }
    }

    public void Kill()
    {
        // TODO animate?
        Destroy(gameObject);
    }

    public void Buy()
    {
        if (!GameManager.main.GameIsOver)
        {
            if (MoneyManager.main.Withdraw(cost) && !PlacementManager.main.IsPlacing)
            {
                PlacementManager.main.SelectItem(this, inputCount, outputCount);
                //Kill();
            }
            else
            {
                if (PlacementManager.main.IsPlacing)
                {
                    PlacementManager.main.UnselectItem();
                }
                SoundManager.main.PlaySound(SoundType.NotEnoughMoney);
            }
        }

    }
}
