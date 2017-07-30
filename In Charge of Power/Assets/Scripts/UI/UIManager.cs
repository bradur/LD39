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

    [SerializeField]
    private RectTransform defaultNotificationPosition;

    public void ShowMouseMessage(string message)
    {
        messageDisplay.SpawnStaticMessage(MousePositionManager.main.GetNormalizedMousePosition(), defaultMessageSprite, message, true, true);
    }

    public void ShowMouseMessage(string message, bool right)
    {
        messageDisplay.SpawnStaticMessage(MousePositionManager.main.GetNormalizedMousePosition(), defaultMessageSprite, message, true, right);
    }

    public void ShowNotification(string message)
    {
        messageDisplay.SpawnMessage(defaultNotificationPosition.anchoredPosition, defaultMessageSprite, message);
    }

    public void ShowNotification(string message, bool stay) {
        if (stay)
        {
            messageDisplay.SpawnMessage(defaultNotificationPosition.anchoredPosition, defaultMessageSprite, message, stay);
        } else
        {
            ShowNotification(message);
        }
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
            //new Vector2(position.x - 50f, position.y),
            position,
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
        if (!GameManager.main.GameIsOver)
        {
            messageDisplay.ClearStaticMessage();
        }
    }

    [SerializeField]
    private HUDCitizens hudCitizens;

    public void AddCitizens (int value)
    {
        hudCitizens.AddValue(value);
    }

    public void SetCitizensTime(float value)
    {
        hudCitizens.SetTimeValue(value);
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

    void SetMoney(float value)
    {
        hudMoney.SetValue(value);
    }

    void Topup(float value)
    {
        hudMoney.Topup(value);
    }

    void Withdraw(float value)
    {
        hudMoney.Withdraw(value);
    }

    public void SetMoneyTime(float value)
    {
        hudMoney.SetTimeValue(value);
    }

    [SerializeField]
    private HUDPower hudPower;

    void AddPower(float amount)
    {
        hudPower.AddPower(amount);
    }

    void DrainPower(float amount)
    {
        hudPower.DrainPower(amount);
    }

    void SetPower(float amount)
    {
        hudPower.SetPower(amount);
    }

    public void SetPowerTime(float value)
    {
        hudPower.SetTimeValue(value);
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

    public void WithdrawResource(float amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            DrainPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            Withdraw(amount);
        }
    }

    public void AddResource(float amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            AddPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            Topup(amount);
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


    public void SetResource(float amount, ResourceType resourceType)
    {
        if (resourceType == ResourceType.Power)
        {
            SetPower(amount);
        }
        else if (resourceType == ResourceType.Money)
        {
            SetMoney(amount);
        }
    }

    [SerializeField]
    private GameObject gameOverScreen;

    public void ShowGameOverScreen ()
    {
        ClearStaticMessage();
        gameOverScreen.SetActive(true);
    }

    [SerializeField]
    private GameObject theEndScreen;

    public void ShowTheEndScreen()
    {
        ClearStaticMessage();
        theEndScreen.SetActive(true);
    }
}
