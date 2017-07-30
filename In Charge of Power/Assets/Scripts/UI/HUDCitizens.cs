// Date   : 29.07.2017 21:04
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDCitizens : MonoBehaviour {

    [SerializeField]
    private Text txtComponent;

    [SerializeField]
    private Text txtTimeComponent;

    private int value = 0;

    private float timeValue = 0;

    public void AddValue (int value)
    {
        this.value += value;
        txtComponent.text = string.Format("{0}", this.value);
    }

    public void SetValue(int value)
    {
        this.value = value;
        txtComponent.text = string.Format("{0}", this.value);
    }

    public void AddTimeValue (float value)
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
        UIManager.main.ShowMouseMessage(string.Format("New citizens per second. Grows as more citizens move into the city."), false);
    }

}
