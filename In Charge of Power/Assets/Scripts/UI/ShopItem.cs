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

    [SerializeField]
    private string itemName = "item";

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

    public void SetKeyCode(KeyCode key)
    {
        correspondingKey = key;
    }

    public void Init()
    {
        imgInput.sprite = ItemManager.main.GetInputResource(itemType).sprite;
        imgOutput.sprite = ItemManager.main.GetOutputResource(itemType).sprite;
        imgItem.sprite = ItemManager.main.GetItem(itemType).sprite;
        if (inputType != ResourceType.None) {
            txtInputCount.text = "" + inputCount;
        }
        txtOutputCount.text = "" + outputCount;
        txtCost.text = string.Format("$ {0}", cost);
        txtName.text = itemName;
    }

    public void HoverIn ()
    {
        CursorManager.main.SetCursor(CursorType.Pointer);
    }

    public void HoverOut ()
    {
        CursorManager.main.SetCursor(CursorType.Default);
    }

    public void DisableShopCapabilities()
    {
        GetComponent<Button>().enabled = false;
        GetComponent<EventTrigger>().enabled = false;
    }

    public void Kill()
    {
        // TODO animate?
        Destroy(gameObject);
    }

    public void Buy()
    {
        if (MoneyManager.main.Withdraw(cost))
        {
            PlacementManager.main.SelectItem(this, inputCount, outputCount);
            DebugLogger.Log(string.Format("You bought a \"{0}\" with {1} dollarydoos.", itemName, cost));
            //Kill();
        }
        else
        {
            SoundManager.main.PlaySound(SoundType.NotEnoughMoney);
        }

    }
}
