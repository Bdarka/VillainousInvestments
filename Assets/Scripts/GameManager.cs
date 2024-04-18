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

    [HideInInspector] public SituationSystem situationSystem;
    [HideInInspector] public bool startSituationSystem;
    [HideInInspector] public TutorialScript Tutorial;
    public List<RivalScript> rivals = new List<RivalScript>();


   // in case I want to make it so the player can name their city
   // public string cityName;

    public Button ShopButton;

    public TextMeshProUGUI playerMoneyDisplay;
    public TextMeshProUGUI rivalMoneyDisplay;
    public Slider landWorthSlider;

    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject MessageWindow;

    public List<BuildingType> playerBuildings;
    public List<BuildingType> rivalBuildings;

    public SFXManager soundManager;

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

        playerBuildings = new List<BuildingType>();
        rivalBuildings = new List<BuildingType>();

        WinScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        MessageWindow.SetActive(false);

        StartCoroutine(MoneyCoroutine());

        Tutorial = this.GetComponentInChildren<TutorialScript>();
        situationSystem = this.GetComponentInChildren<SituationSystem>();
        soundManager = this.GetComponentInChildren<SFXManager>();

        Tutorial.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();

        if(landWorth >= 300)
        {
            GameOverScreen.SetActive(true);
        }

        if(startSituationSystem == true)
        {
            situationSystem.gameObject.SetActive(true);
        }

        if(playerMoney == 10000)
        {
            WinScreen.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    #region Building Management

    public void BuildingTracking(BuildingType buildingType, PlaceableObject placeableObject)
    {
        if(placeableObject.playersBuilding == true)
        {
            playerMoney -= buildingType.BuildingCost + (int)landWorth;
            playerIncome += buildingType.BuildingPayOut;
            landWorth += buildingType.BuildingLandWorth;
            playerBuildings.Add(buildingType);
            soundManager.PlaySound(buildingType.buildingName.ToString());
        }
        else
        {
            rivals[0].rivalMoney -= buildingType.BuildingCost + (int)landWorth;
            rivals[0].rivalIncome += buildingType.BuildingPayOut;
            landWorth += buildingType.BuildingLandWorth;
            rivalBuildings.Add(buildingType);
        }
    }

    public void SellBuilding(BuildingType buildingType)
    {
        playerMoney += buildingType.BuildingSellPrice;
        playerIncome -= buildingType.BuildingPayOut;
        landWorth -= buildingType.BuildingLandWorth;
        playerBuildings.Remove(buildingType);
    }

    #endregion

    IEnumerator MoneyCoroutine()
    {
        while (true)
        {
            playerMoney += playerIncome;

            foreach (RivalScript rival in rivals)
            {
                rival.rivalMoney += rival.CalculateIncome();
            }

            yield return new WaitForSeconds(payInterval);
        }
    }

    public void SetUI()
    {
        playerMoneyDisplay.text = "$" + playerMoney;
        landWorthSlider.value = landWorth;
        rivalMoneyDisplay.text = "$" + rivals[0].rivalMoney;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
