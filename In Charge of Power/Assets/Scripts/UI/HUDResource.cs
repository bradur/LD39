// Date   : 29.07.2017 23:09
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDResource : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;
    [SerializeField]
    private Color colorVariable;
    [SerializeField]
    private Image imgComponent;

    private int currentValue;

    public void SetValue(int value)
    {
        currentValue = value;
        txtComponent.text = string.Format("{0}", currentValue);
    }

    public void AddValue(int value)
    {
        currentValue += value;
        txtComponent.text = string.Format("{0}", currentValue);
    }

    public void Withdraw(int value)
    {
        currentValue -= value;
        txtComponent.text = string.Format("{0}", currentValue);
    }
}
