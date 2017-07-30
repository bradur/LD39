// Date   : 29.07.2017 16:20
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDMoney : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    [SerializeField]
    private float currentMoney = 0;


    void Start()
    {
        currentMoney = MoneyManager.main.GetBalance();
        SetValue(currentMoney);
    }

    public void SetValue(float value)
    {
        currentMoney = value;
        txtComponent.text = string.Format("${0:#.00}", currentMoney);
    }

    public void Topup(float value)
    {
        currentMoney += value;
        txtComponent.text = string.Format("${0:#.00}", currentMoney);
    }

    public void Withdraw(float value)
    {
        currentMoney -= value;
        txtComponent.text = string.Format("${0:#.00}", currentMoney);
    }

    [SerializeField]
    private Text txtTimeComponent;

    private float timeValue = 0;

    public void AddTimeValue(float value)
    {
        this.timeValue += value;
        txtTimeComponent.text = string.Format("{0:#.00}", this.timeValue);
    }

    public void SetTimeValue(float value)
    {
        this.timeValue = value;
        txtTimeComponent.text = string.Format("{0:#.00}", this.timeValue);
    }

    public void ShowStaticTimeMessage()
    {
        UIManager.main.ShowMouseMessage(string.Format("Money per second. More citizens = more energy consumption = more money."), true);
    }
}
