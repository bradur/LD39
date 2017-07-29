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
        Topup(currentMoney);
    }

    public void Topup(int totalAfterTopup)
    {
        currentMoney = totalAfterTopup;
        txtComponent.text = string.Format("${0}", totalAfterTopup);
    }

    public void Withdraw(int totalAfterWithdraw)
    {
        currentMoney = totalAfterWithdraw;
        txtComponent.text = string.Format("${0}", totalAfterWithdraw);
    }

}
