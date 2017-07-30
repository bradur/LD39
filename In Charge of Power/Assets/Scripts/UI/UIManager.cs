// Date   : 29.07.2017 16:18
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

    public static UIManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("UIManager").Length == 0)
        {
            main = this;
            gameObject.tag = "UIManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    private MessageDisplay messageDisplay;

    [SerializeField]
    private Sprite defaultMessageSprite;

    public void ShowMouseMessage(string message)
    {
        messageDisplay.SpawnStaticMessage(Input.mousePosition, defaultMessageSprite, message, true);
    }

    public void ShowDefaultMessage(Vector2 position, string message)
    {
        messageDisplay.SpawnStaticMessage(position, defaultMessageSprite, message, false);
    }

    public void ShowResourceMessage(Vector2 position, int amount, ResourceType resourceType)
    {
        if(resourceType == ResourceType.None)
        {
            return;
        }
        Sprite sprite = ResourceManager.main.GetResource(resourceType).sprite;
        float width = 100f;
        messageDisplay.SpawnMessage(
            new Vector2(position.x - 50f, position.y),
            sprite,
            string.Format(
                "{0}{1}",
                amount > 0 ? "+" : "",
                amount
            ),
            width
        );
    }

    public void ShowMoneyMessage(Vector2 position, int amount)
    {
        Sprite moneySprite = ResourceManager.main.GetResource(ResourceType.Money).sprite;
        messageDisplay.SpawnMessage(
            position,
            moneySprite,
            string.Format(
                "{0}{1}",
                amount > 0 ? "+" : "",
                amount
            )
        );
    }

    public void ClearStaticMessage()
    {
        messageDisplay.ClearStaticMessage();
    }

    [SerializeField]
    private HUDToggle hudMusic;

    [SerializeField]
    private HUDToggle hudSfx;

    public void ToggleMusic()
    {
        hudMusic.Toggle();
    }

    public void ToggleSfx()
    {
        hudSfx.Toggle();
    }

    [SerializeField]
    private HUDResource hudCoal;
    [SerializeField]
    private HUDResource hudNuclear;


    [SerializeField]
    private HUDMoney hudMoney;

    void SetMoney(int value)
    {
        hudMoney.SetValue(value);
    }

    void Topup(int value)
    {
        hudMoney.Topup(value);
    }

    void Withdraw(int value)
    {
        hudMoney.Withdraw(value);
    }

    [SerializeField]
    private HUDPower hudPower;

    void AddPower(int amount)
    {
        hudPower.AddPower(amount);
    }

    void DrainPower(int amount)
    {
        hudPower.DrainPower(amount);
    }

    void SetPower(int amount)
    {
        hudPower.SetPower(amount);
    }

    public void WithdrawResource(int amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            DrainPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            Withdraw(amount);
        }
        else if (resourceType == ResourceType.Coal)
        {
            hudCoal.Withdraw(amount);
        }
        else if (resourceType == ResourceType.Nuclear)
        {
            hudNuclear.Withdraw(amount);
        }
    }

    public void AddResource(int amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            AddPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            Topup(amount);
        }
        else if (resourceType == ResourceType.Coal)
        {
            hudCoal.AddValue(amount);
        }
        else if (resourceType == ResourceType.Nuclear)
        {
            hudNuclear.AddValue(amount);
        }
    }

    public void SetResource(int amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            SetPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            SetMoney(amount);
        }
        else if (resourceType == ResourceType.Coal)
        {
            hudCoal.SetValue(amount);
        }
        else if (resourceType == ResourceType.Nuclear)
        {
            hudNuclear.SetValue(amount);
        }
    }
}
