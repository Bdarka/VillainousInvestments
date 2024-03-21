using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SituationSystem : MonoBehaviour
{
    public GameObject MessageWindow;
    public TextMeshProUGUI EventText;

    public float eventTimer;
    public int randomRoll;

    public GameManager GameManager;

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
                    break;

                case 1:
                    // Event 2
                    break;

                case 2:
                    // Event 3
                    break;
                case 3:
                    // Event 4
                    break;
                case 4:
                    break;

                default:
                    break;
            }
        }  
    }

    #region Event Functions

    public void Event1()
    {
        // Do something, change variables in Game Manager
        GameManager.playerMoney -= 200;
        GameManager.SetUI();
    }

    public void Event2()
    {
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

    #endregion

    public void DisplayEventWindow()
    {
        MessageWindow.SetActive(true);
    }

    public void CloseEventWindow()
    {
        MessageWindow.SetActive(false);
        eventTimer = Random.Range(30, 300);
    }
}
