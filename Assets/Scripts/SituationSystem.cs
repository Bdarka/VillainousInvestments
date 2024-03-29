using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SituationSystem : MonoBehaviour
{
    public GameObject MessageWindow;
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventText;
    public GameObject AcceptButton;
    public GameObject NextButton;

    public float eventTimer;
    public int randomRoll;

    [HideInInspector] public GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = gameObject.GetComponent<GameManager>();
        eventTimer = 15;
    }

    // Update is called once per frame
    void Update()
    {

        if(MessageWindow.activeSelf == false)
        {
            eventTimer -= Time.deltaTime;
        }


        //I know the second conditional should never occur but I wanted to have some redundancy
        if (eventTimer <= 0.0f && MessageWindow.activeSelf == true)
        {
            randomRoll = Random.Range(0, 4);

            switch(randomRoll)
            {
                case 0:
                    // Event 1
                    Event1();
                    break;

                case 1:
                    // Event 2
                    Event2();
                    break;

                case 2:
                    // Event 3
                    Event3();
                    break;
                case 3:
                    // Event 4
                    Event4();
                    break;
                case 4:
                    Event5();
                    break;

                default:
                    break;
            }
        }  
    }

    #region Event Functions

    public void Event1()
    {
        // Do something, change variables in Game Manager. Same for every event
        DisplayEventWindow("Accept");

        EventName.text = "Fraud Report";
        EventText.text = "A goody two shoes showed the IRS your taxes. Pay off your accountant to make it go away";

        GameManager.playerMoney -= 200;

        GameManager.SetUI();
    }

    public void Event2()
    {
        DisplayEventWindow("Accept");

        EventName.text = "Monster Rumor";
        EventText.text = "The locals believed your " + '\u0022' + "true story" + '\u0022' + " about a monster running wild." +
                            '\n' + "They've fled the town in earnest" + '\n' + "Land Worth - 20";
        // could also put "The locals believed your \"true story" but I wanna try the ASCII too
        
        GameManager.landWorth -= 20;

        GameManager.SetUI();
    }

    public void Event3()
    {


        GameManager.SetUI();
    }

    public void Event4()
    {
        GameManager.SetUI();
    }

    public void Event5()
    {
        GameManager.SetUI();
    }

    public void RivalPlacedBuilding()
    {
        EventName.text = "Rival Investor";
        EventText.text = "A Rival placed a building! Get rid of them!";
    }

    #endregion


    #region Event Window Display
    public void DisplayEventWindow(string buttonName)
    {
        MessageWindow.SetActive(true);
        EventText.text = "";
        AcceptButton.SetActive(false);
        NextButton.SetActive(false);

        if(buttonName == "Accept")
        {
            AcceptButton.SetActive(true);
        }
        else if(buttonName == "Next")
        {
            NextButton.SetActive(true);
        }
    }

    public void CloseEventWindow()
    {
        EventName.text = "";
        EventText.text = "";
        MessageWindow.SetActive(false);
        eventTimer = Random.Range(30, 300);
    }

    #endregion
}
