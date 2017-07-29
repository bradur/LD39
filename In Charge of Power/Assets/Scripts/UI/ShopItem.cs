// Date   : 29.07.2017 16:09
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{

    [SerializeField]
    private int cost = 0;

    [SerializeField]
    private string itemName = "item";

    [SerializeField]
    private ItemType itemType = ItemType.None;

    //[SerializeField]
    private KeyCode correspondingKey;

    [SerializeField]
    private ResourceType inputType;

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

    void Start()
    {
        txtInputCount.text = "" + inputCount;
        imgInput.sprite = ItemManager.main.GetInputResource(itemType).sprite;
        imgOutput.sprite = ItemManager.main.GetOutputResource(itemType).sprite;
        txtOutputCount.text = "" + outputCount;
    }

    void Update()
    {

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
            PlacementManager.main.SelectItem(ItemManager.main.GetItem(itemType), inputCount, outputCount);
            DebugLogger.Log(string.Format("You bought a \"{0}\" with {1} dollarydoos.", itemName, cost));
            Kill();
        }
        else
        {
            SoundManager.main.PlaySound(SoundType.NotEnoughMoney);
        }

    }
}
