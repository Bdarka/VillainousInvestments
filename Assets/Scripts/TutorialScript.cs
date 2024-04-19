using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;
using System;

public class TutorialScript : MonoBehaviour
{
    public GameManager gameManager;
    public SituationSystem situationSystem;
    public BuildingSystem buildingSystem;

    public bool displayTutorial;
    public int tutorialLevel;
    public int textCount;

    public GameObject MessageWindow;
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventText;

    public RivalScript rival;
    public TextMeshProUGUI rivalMoney;
    public GameObject rivalMoneyBackground;

    // Start is called before the first frame update
    void Start()
    {
        tutorialLevel = 1;
        textCount = 0;
        displayTutorial = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(displayTutorial == true)
        {
            switch (tutorialLevel)
            {
                case 1:
                    {
                        Tutorial1(textCount);
                        break;
                    }

                case 2:
                    {
                        Tutorial2(textCount);
                        break;
                    }
                case 3:
                    {
                        Tutorial3(textCount); 
                        break;
                    }
                case 4:
                    {
                        Tutorial4(textCount);
                        break;
                    }
            }
        }

        if(tutorialLevel == 1 && textCount == 2)
        {
            if(gameManager.playerBuildings.Count > 0)
            {
                BuildingType bt = gameManager.playerBuildings[0];
                

                if(bt.buildingName == BuildingType.BuildingName.Swamp)
                {
                    EventName.text = "Tutorial #1";
                    EventText.text = "No no no you need to select the office. Try Again";
                    SmiteBuilding(bt);
                }

                else
                {
                    textCount++;
                    displayTutorial = true;
                }
            }
        }

        if(tutorialLevel == 2 && textCount == 3)
        {
            BuildingType bt = gameManager.playerBuildings[1];

            if(bt.buildingName == BuildingType.BuildingName.Office)
            {

                EventName.text = "Tutorial #2";
                EventText.text = "Don't get ahead of yourself. Make a Swamp first and then we can go back to making money";
                SmiteBuilding(bt);
            }

            else if(bt != null)
            {
                textCount++;
                displayTutorial = true;
            }
        }

        if(tutorialLevel == 3  && textCount == 3 && MessageWindow.activeSelf == false)
        {
            displayTutorial = true;
        }
    }

    #region Tutorial Functions 
    public void IncrementTextCount()
    {
        textCount++;
    }

    public void ResetTextCount()
    {
        textCount = 0;
    }

    public void SmiteBuilding(BuildingType bt)
    {
        PlaceableObject po = gameManager.playerBuildings[0].GetComponent<PlaceableObject>();
        situationSystem.DisplayEventWindow("Accept");

        gameManager.playerMoney += bt.BuildingCost;
        gameManager.landWorth -= bt.BuildingLandWorth;

        Vector3Int start = buildingSystem.gridLayout.WorldToCell(po.GetStartPosition());
        buildingSystem.FreeArea(start, po.Size);

        Destroy(gameManager.playerBuildings[0].gameObject);

        gameManager.playerBuildings.Clear();
    }

    #endregion

    #region Tutorial 1

    public void Tutorial1(int textCount)
    {
        switch (textCount)
        {
            case 0:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Tutorial #1";
                    EventText.text = "Welcome to Villainous Investments!";
                    break;
                }
            case 1:
                {
                    EventName.text = "Tutorial #1";
                    EventText.text = "To place a building, first buy it from the shop. \nClick the shop button and select office";

                    break;
                }
            case 2:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    
                    EventName.text = "Tutorial #1";
                    EventText.text = "Use the R button to rotate buildings before placing them. \nPress the spacebar to place it";

                    displayTutorial = false;
                    textCount++;
                    break;
                }
            case 3:
                {
                    situationSystem.DisplayEventWindow("Next");
                    EventName.text = "Tutorial #1";
                    EventText.text = "Fantastic! Your evil empire is getting started";
                    break;
                }
            case 4:
                {
                    situationSystem.DisplayEventWindow("Next");
                    EventName.text = "Tutorial #1";
                    EventText.text = "You can move the camera with either the arrow keys or WASD";
                    break;
                }

            case 5:
                {
                    situationSystem.DisplayEventWindow("Next");
                    EventName.text = "Tutorial #1";
                    EventText.text = "If you misclick in the shop you can press Backspace to fix your mistake";
                    break;
                }

            case 6:
                {
                    situationSystem.DisplayEventWindow("Accept");

                    EventName.text = "Tutorial #1";
                    EventText.text = "Watch your money go up as you collect rent from your tenants!";

                    tutorialLevel++;
                    ResetTextCount();
                    gameManager.playerMoney += 200;
                    //  displayTutorial = false;
                    break;
                }
            default:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    textCount = 0;
                    displayTutorial = false;
                    break;
                }
        }

    }

   

    #endregion

    #region Tutorial 2
    public void Tutorial2(int textCount)
    {
        switch (textCount)
        {
            case 0:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Tutorial #2";
                    EventText.text = "Payouts can take some time, so for this tutorial we're gonna give you a little extra cash";
                    break;
                }
            case 1:
                {
                    EventName.text = "Tutorial #2";
                    EventText.text = "Keep an eye on that slider to the bottom right. That's your current Land Worth.";
                    break;
                }
            case 2:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Tutorial #2";
                    EventText.text = "If it gets too high this area will become lucrative to other investors, destroying your dastardly schemes";
                    break;
                }
            case 3:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    EventName.text = "Tutorial #2";
                    EventText.text = "Try placing a Swamp down to make up for that beautiful office building. Don't put them too close together, or the office will pay out less money";

                    textCount++;
                    displayTutorial = false;
                    break;
                }
            case 4:
                {
                    situationSystem.DisplayEventWindow("Accept");

                    EventName.text = "Tutorial #2";
                    EventText.text = "Good job placing that Swamp! Maybe we can use it's negative influence for other nefarious deeds later...";
                    ResetTextCount();
                    tutorialLevel++;
                    gameManager.landWorth -= 20;
                    break;
                }

            default:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    textCount = 0;
                    displayTutorial = false;
                    break;
                }
        }

    }

    #endregion

    #region Tutorial 3

    public void Tutorial3(int textCount)
    {
        switch (textCount)
        {
            case 0:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Tutorial #3";
                    EventText.text = "During the course of your villainous schemes various events can occur. For instance...";

                    break;
                }
            case 1:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Monster Rumor";
                    EventText.text = "The locals believed your   \"true story" + " about a monster running wild." +
                                                   '\n' + "They've fled the town in earnest" + '\n' + "Land Worth - 20";

                    break;
                }
            case 2:
                {
                    situationSystem.DisplayEventWindow("Accept");

                    EventName.text = "Tutorial #3";
                    EventText.text = "Not all events will be in your favor. Enjoy this first freebie and start building your empire.";

                    tutorialLevel++;
                    ResetTextCount();
                    gameManager.GetComponent<SituationSystem>().startEvents = true;
                    break;
                }

            default:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    textCount = 0;
                    displayTutorial = false;
                    break;
                }
        }

    }

    #endregion

    #region Tutorial 4
    public void Tutorial4(int textCount) 
    {
        switch (textCount)
        {
            case 0:
                {
                    situationSystem.DisplayEventWindow("Next");

                    EventName.text = "Tutorial #4";
                    EventText.text = "Oh no looks looks like someone else is trying to edge in on your turf";
                    rivalMoney.text = "$" + rivalMoney.ToString();
                    rivalMoneyBackground.SetActive(true);
                    break;
                }

            case 1:
                {
                    situationSystem.DisplayEventWindow("Next");
                    EventName.text = "Tutorial #4";
                    EventText.text = "Try bankrupting them by putting swamps around their buildings";

                    break;
                }

            case 2:
                {
                    situationSystem.DisplayEventWindow("Next");
                    EventName.text = "Tutorial #4";
                    EventText.text = "Just make sure you don't ruin your own buildings in the process";

                    break;
                }
        

            case 3:
                {
                    rival.gameObject.SetActive(true);
                    break;
                }

            default:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    textCount = 0;
                    displayTutorial = false;
                    break;
                }
        }
    }

    #endregion
}
