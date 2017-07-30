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
    [Range(0, 1000)]
    private int power = 0;

    [SerializeField]
    [Range(1, 10)]
    private int passivePowerDrainPerCitizen = 1;

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

    public void AddPower(int amount)
    {
        power += amount;
        UIManager.main.AddResource(amount, ResourceType.Power);
    }

    public bool DrainPower(int amount)
    {
        // TODO CHECK FOR ZERO
        if ((power - amount) >= 0)
        {
            power -= amount;
            UIManager.main.WithdrawResource(amount, ResourceType.Power);
            ResourceManager.main.AddResource(amount * moneyPerPower, ResourceType.Money);
            return true;
        }
        return false;
    }

    public void RecalculateRate()
    {
        UIManager.main.SetPowerTime(PlacementManager.main.GetRate(ResourceType.Power) + GetPowerDrainRate() * CitizenManager.main.GetCitizenCount());
    }

    public float GetPowerDrainRate()
    {
        return GetPowerDrainPerCitizen() / passivePowerDrainInterval;
    }

    public int GetPowerDrainPerCitizen()
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
