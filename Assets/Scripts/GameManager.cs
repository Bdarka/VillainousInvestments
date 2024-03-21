using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int playerMoney;
    public int playerIncome;
    public int payInterval;

    public float landWorth;
    public float fearLevel;

    public GameObject Rival1;

    string cityName;

    public Button ShopButton;

    public TextMeshProUGUI playerMoneyDisplay;
    public Slider landWorthSlider;
    public Slider fearLevelSlider;

    public GameObject GameOverScreen;

    public List<BuildingType> buildings;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        playerMoney = 100;
        playerMoneyDisplay.text = "$" + playerMoney;
        playerIncome = 0;

        landWorth = 0;
        landWorthSlider.value = 0;

        buildings = new List<BuildingType>();

        GameOverScreen.SetActive(false);

        StartCoroutine(MoneyCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();

        if(landWorth == 100)
        {
            GameOverScreen.SetActive(true);
        }

        if(playerMoney == 100000)
        {
            Rival1.SetActive(true);
        }
    }

    #region Building Management

    public void BuildingTracking(BuildingType buildingType)
    {
        playerMoney -= buildingType.BuildingCost;
        playerIncome += buildingType.BuildingPayOut;
        landWorth += buildingType.BuildingLandWorth;
        buildings.Add(buildingType);
    }

    public void SellBuilding(BuildingType buildingType)
    {
        playerMoney += buildingType.BuildingSellPrice;
        playerIncome -= buildingType.BuildingPayOut;
        landWorth -= buildingType.BuildingLandWorth;
        buildings.Remove(buildingType);
    }

    #endregion

    IEnumerator MoneyCoroutine()
    {
        while (true)
        {
            playerMoney += playerIncome;
            yield return new WaitForSeconds(payInterval);
            Debug.Log((playerMoney));
        }
    }

    public void SetUI()
    {
        playerMoneyDisplay.text = "$" + playerMoney;
        landWorthSlider.value = landWorth;
    }
}
