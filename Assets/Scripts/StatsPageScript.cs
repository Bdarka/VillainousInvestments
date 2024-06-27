using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsPageScript : MonoBehaviour
{
    // Object References
    public GameManager GameManager;
    public TimeManagerScript TimeManager;
    public CitizenManager CitizenManager;

    public int currentYear;
    public int prevYear;

    // Player Money Stats 
    public float currentMoney;
    public float totalMoney;
    public float YTDMoney;

    // Citizen Money Stats
    public float citizenMoneyAVG;
    public float citizenTotalMoney;
    public float citizenRentAVG;
    public float citizenTotalRent;


    // Building Tracking
    public int buildingsTotal;
    public GameObject buildingMostProfit;
    public float buildingsProfitAVG;
    public float buildingsProfitYTD;

    // Land Worth Stats 
    public float currentLandWorth;
    public float landWorthAVG;

    #region Text Meshes 
    public TextMeshProUGUI currentMoneyDisplay;
    public TextMeshProUGUI totalMoneyDisplay;
    public TextMeshProUGUI buildingsCountDisplay;
    public TextMeshProUGUI buildingMostProfitDisplay;

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupStats()
    {
        totalMoney = GameManager.playerMoney;
        YTDMoney = currentMoney;
        UpdateBaseStats();
        currentYear = TimeManager.year;
    }

    public void TotalMoneyCount(float newMoney)
    {
        totalMoney += newMoney;
        currentMoney = GameManager.playerMoney;
        UpdateYTDStats(TimeManager.year);
    }

    public void UpdateBaseStats()
    {
        buildingsTotal = GameManager.playerBuildings.Count;
    }

    public void UpdateYTDStats(int year)
    {
        if(year == currentYear)
        {
            YTDMoney += GameManager.playerIncome;
        }

        else
        {
            YTDMoney = 0;
            currentYear = year;
        }
    }

    public void SetUI()
    {
        UpdateBaseStats();

        currentMoneyDisplay.text = "$" + currentMoney;
    }
}
