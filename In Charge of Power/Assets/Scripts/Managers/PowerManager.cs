// Date   : 29.07.2017 21:12
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class PowerManager : MonoBehaviour
{

    public static PowerManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("PowerManager").Length == 0)
        {
            main = this;
            gameObject.tag = "PowerManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    [Range(0f, 1000f)]
    private float power = 0;

    [SerializeField]
    [Range(0.01f, 10f)]
    private float passivePowerDrainPerCitizen = 0.01f;

    [SerializeField]
    [Range(1, 10)]
    private int moneyPerPower = 1;

    [SerializeField]
    [Range(0.2f, 5f)]
    private float passivePowerDrainInterval = 0.2f;

    private float timer = 0f;

    void Start()
    {
        UIManager.main.SetResource(power, ResourceType.Power);
    }

    public void AddPower(float amount)
    {
        power += amount;
        UIManager.main.AddResource(amount, ResourceType.Power);
    }

    public bool DrainPower(float amount)
    {
        // TODO CHECK FOR ZERO
        if ((power - amount) >= 0)
        {
            power -= amount;
            UIManager.main.WithdrawResource(amount, ResourceType.Power);
            ResourceManager.main.AddResource(amount * moneyPerPower, ResourceType.Money);
            return true;
        } else
        {
            GameManager.main.GameOver();
        }
        return false;
    }

    public int GetMoneyPerPower()
    {
        return moneyPerPower;
    }

    public void RecalculateRate()
    {
        UIManager.main.SetPowerTime(PlacementManager.main.GetRate(ResourceType.Power) + GetPowerDrainRate() * CitizenManager.main.GetCitizenCount());
    }

    public float GetPowerDrainRate()
    {
        return GetPowerDrainPerCitizen() / passivePowerDrainInterval;
    }

    public float GetPowerDrainPerCitizen()
    {
        return -passivePowerDrainPerCitizen;
    }

    void Update()
    {
        if (timer < passivePowerDrainInterval)
        {
            timer += Time.deltaTime;
        }
        else
        {
            DrainPower(passivePowerDrainPerCitizen * CitizenManager.main.GetCitizenCount());
            timer = 0f;
        }
    }
}
