using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Net.Http.Headers;

public class TutorialScript : MonoBehaviour
{
    public GameManager gameManager;
    public SituationSystem situationSystem;

    public bool displayTutorial;
    public int tutorialLevel;
    public int textCount;

    public GameObject MessageWindow;
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventText;

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
            }
        }
    }

    #region Tutorial Functions 
    public void IncrementTutorialCount()
    {
        textCount++;
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
                    EventText.text = "Use the R button to rotate buildings before placing them. \n Press the spacebar to place it";
                    break;
                }
            case 2:
                {
                    situationSystem.DisplayEventWindow("Accept");
                    
                    EventName.text = "Tutorial #1";
                    EventText.text = "To place a building, first buy from the shop.";

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
                    situationSystem.DisplayEventWindow("Accept");

                    EventName.text = "Tutorial #1";
                    EventText.text = "Watch your money go up as you collect rent from your tenants!";

                    textCount = 0;
                    displayTutorial = false;
                    tutorialLevel++;
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
        
    }

    #endregion
}
