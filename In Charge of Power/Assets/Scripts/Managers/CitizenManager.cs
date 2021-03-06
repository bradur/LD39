// Date   : 30.07.2017 16:33
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using System.Collections;

public class CitizenManager : MonoBehaviour
{

    public static CitizenManager main;

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("CitizenManager").Length == 0)
        {
            main = this;
            gameObject.tag = "CitizenManager";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField]
    [Range(0f, 100f)]
    private float newCitizensMoveInterval = 3f;

    private float newCitizenMoveTimer = 0f;

    [SerializeField]
    [Range(0f, 190f)]
    private float newCitizenBirthInterval = 20f;

    private float newCitizenBirthTimer = 0f;

    private int amountOfCitizens = 0;

    [SerializeField]
    [Range(1, 100)]
    private int citizenMoveGrowthPercentage = 10;

    [SerializeField]
    [Range(1, 100)]
    private int citizenBirthGrowthPercentage = 25;

    [SerializeField]
    [Range(1f, 500f)]
    private float firstWaveTimer = 2f;

    [SerializeField]
    [Range(2, 200)]
    private int firstWaveOfCitizens;

    [SerializeField]
    private bool DEBUG = false;

    private float CalculateNewCitizensMovingInPerSecond()
    {
        int citizens = (amountOfCitizens * citizenMoveGrowthPercentage / 100);
        if (citizens < 1)
        {
            citizens = 1;
        }
        return citizens / newCitizensMoveInterval;
    }

    private float CalculateNewCitizensBeingBornPerSecond()
    {
        int citizens = (amountOfCitizens * citizenBirthGrowthPercentage / 100);
        if (citizens < 1)
        {
            citizens = 1;
        }
        return citizens / newCitizenBirthInterval;
    }

    void Start()
    {
        UIManager.main.ShowNotification(string.Format("The first new citizens will move into the city in {0} seconds!", (int)firstWaveTimer), true);
    }

    void Update()
    {
        if (firstWaveTimer > 0f)
        {
            firstWaveTimer -= Time.deltaTime;
            if (firstWaveTimer < 0f || DEBUG)
            {
                if (DEBUG)
                {
                    firstWaveTimer = -1f;
                }
                FirstWaveMovesIn(firstWaveOfCitizens);
            }
        }
        else
        {
            if (newCitizenMoveTimer < newCitizensMoveInterval)
            {
                newCitizenMoveTimer += Time.deltaTime;
            }
            else
            {
                newCitizenMoveTimer = 0f;
                NewCitizensMoveIn();
            }
            if (newCitizenBirthTimer < newCitizenBirthInterval)
            {
                newCitizenBirthTimer += Time.deltaTime;
            }
            else
            {
                newCitizenBirthTimer = 0f;
                NewCitizensAreBorn();
            }
        }
    }

    public int GetCitizenCount()
    {
        return amountOfCitizens;
    }

    void FirstWaveMovesIn(int newCitizens)
    {
        AddCitizens(newCitizens);
        UIManager.main.AddCitizens(newCitizens);
        UIManager.main.ShowNotification(string.Format(
            "The first {0} citizens have moved into the city!",
            newCitizens
        ));
        InformCitizenChangeToUI();
    }

    void InformCitizenChangeToUI()
    {
        UIManager.main.SetCitizensTime(CalculateNewCitizensMovingInPerSecond() + CalculateNewCitizensBeingBornPerSecond());
        PowerManager.main.RecalculateRate();
        MoneyManager.main.RecalculateRate();
    }

    [SerializeField]
    [Range(5, 500)]
    int victoryCondition = 100;
    void AddCitizens (int amount)
    {
        amountOfCitizens += amount;
        if (amountOfCitizens >= victoryCondition)
        {
            GameManager.main.TheEnd();
        }
    }

    void NewCitizensMoveIn()
    {
        int newCitizens = Mathf.Clamp(amountOfCitizens * citizenMoveGrowthPercentage / 100, 1, 100);
        AddCitizens(newCitizens);
        UIManager.main.AddCitizens(newCitizens);
        UIManager.main.ShowNotification(string.Format(
            "{0} new citizen{1} {2} moved into the city!",
            newCitizens == 1 ? "a" : "" + newCitizens,
            newCitizens == 1 ? "" : "s",
            newCitizens == 1 ? "has" : "have"
        ));
        InformCitizenChangeToUI();
    }

    void NewCitizensAreBorn()
    {
        int newCitizens = Mathf.Clamp(amountOfCitizens * citizenBirthGrowthPercentage / 100, 1, 100);
        AddCitizens(newCitizens);
        UIManager.main.AddCitizens(newCitizens);
        UIManager.main.ShowNotification(string.Format(
            "{0} new citizen{1} {2} been born in the city!",
            newCitizens == 1 ? "a" : "" + newCitizens,
            newCitizens == 1 ? "" : "s",
            newCitizens == 1 ? "has" : "have"
        ));
        InformCitizenChangeToUI();
    }
}
