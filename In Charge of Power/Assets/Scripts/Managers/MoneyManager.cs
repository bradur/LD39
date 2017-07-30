// Date   : 29.07.2017 16:12
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class MoneyManager : MonoBehaviour
{

    [SerializeField]
    [Range(0, 10000)]
    private int money = 0;

    public static MoneyManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("MoneyManager").Length == 0)
        {
            main = this;
            gameObject.tag = "MoneyManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool Withdraw(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            UIManager.main.WithdrawResource(amount, ResourceType.Money);
            return true;
        }
        return false;
    }

    public void Topup(int amount)
    {
        money += amount;
        UIManager.main.AddResource(amount, ResourceType.Money);
    }

    public int GetBalance()
    {
        return money;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
