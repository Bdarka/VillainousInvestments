using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Manager References
    public static GameManager instance;
    [HideInInspector] public SituationSystem situationSystem;
    public SFXManager soundManager;
    public TimeManagerScript timeManager;
    public StatsPageScript statsPageScript;

    // Player Resource Tracking
    public int playerMoney;
    public int playerIncome;
    public int payInterval;
    public List<BuildingType> playerBuildings;
    public float landWorth;
    public string cityName;

    // Tutorial Components
    [HideInInspector] public bool startSituationSystem;
    [HideInInspector] public TutorialScript Tutorial;
    private bool demoBugFix;

    // Rival Tracking
    public List<BuildingType> rivalBuildings;
    public List<RivalScript> rivals = new List<RivalScript>();

    // HUD Displays
    public TextMeshProUGUI playerMoneyDisplay;
    public TextMeshProUGUI rivalMoneyDisplay;
    public Slider landWorthSlider;

    // Message Displays
    public GameObject WinScreen;
    public GameObject GameOverScreen;
    public GameObject MessageWindow;
    public GameObject OptionsMenu;


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
        playerMoney += 100;
        playerMoneyDisplay.text = "$" + playerMoney;
        playerIncome = 0;

        landWorth = 0;
        landWorthSlider.value = 0;

        playerBuildings = new List<BuildingType>();
        rivalBuildings = new List<BuildingType>();

        WinScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        MessageWindow.SetActive(false);

        Tutorial = this.GetComponentInChildren<TutorialScript>();
        situationSystem = this.GetComponentInChildren<SituationSystem>();
        soundManager = this.GetComponentInChildren<SFXManager>();
        timeManager = this.GetComponent<TimeManagerScript>();

        StartCoroutine(MoneyCoroutine());

        Tutorial.gameObject.SetActive(true);
        demoBugFix = true;

        statsPageScript.SetupStats();
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();

        if(landWorth >= 100)
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
            OptionMenu();
        }

        if(Input.GetKeyUp(KeyCode.Insert) && demoBugFix == true)
        {
            playerMoney += 200;
            demoBugFix = false;
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
        while (timeManager.isPaused == false)
        {
            playerMoney += playerIncome;

            statsPageScript.TotalMoneyCount(playerIncome);

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


    public void OptionMenu()
    {
        if (OptionsMenu.activeSelf == false)
        {
            OptionsMenu.SetActive(true);
            timeManager.isPaused = true;
        }
        else
        {
            OptionsMenu.SetActive(false);
            timeManager.isPaused = false;
        }
    }
}
