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
