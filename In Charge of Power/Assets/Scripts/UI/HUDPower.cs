// Date   : 29.07.2017 21:04
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDPower : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;

    private int power = 0;

    [SerializeField]
    private Text txtTimeComponent;

    private float timeValue = 0;

    public void AddPower (int value)
    {
        power += value;
        txtComponent.text = string.Format("{0}", power);
    }

    public void SetPower(int value)
    {
        power = value;
        txtComponent.text = string.Format("{0}", power);
    }

    public void DrainPower(int value)
    {
        power -= value;
        txtComponent.text = string.Format("{0}", power);
    }

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
}
