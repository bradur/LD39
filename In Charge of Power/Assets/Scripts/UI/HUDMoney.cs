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
    private int currentMoney = 0;


    void Start()
    {
        currentMoney = MoneyManager.main.GetBalance();
        SetValue(currentMoney);
    }

    public void SetValue(int value)
    {
        currentMoney = value;
        txtComponent.text = string.Format("${0}", currentMoney);
    }

    public void Topup(int value)
    {
        currentMoney += value;
        txtComponent.text = string.Format("${0}", currentMoney);
    }

    public void Withdraw(int value)
    {
        currentMoney -= value;
        txtComponent.text = string.Format("${0}", currentMoney);
    }

}
