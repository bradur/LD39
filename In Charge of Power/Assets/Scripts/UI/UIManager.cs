// Date   : 29.07.2017 16:18
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

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
    private HUDMoney hudMoney;

    public void Topup (int totalAfterTopup)
    {
        hudMoney.Topup(totalAfterTopup);
    }

    public void Withdraw(int totalAfterWithdraw)
    {
        hudMoney.Topup(totalAfterWithdraw);
    }

    void Start () {
    
    }

    void Update () {
    
    }
}
