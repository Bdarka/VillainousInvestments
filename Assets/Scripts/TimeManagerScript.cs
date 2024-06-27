using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManagerScript : MonoBehaviour
{
    public float time;

    public int hour;
    public int day;
    public int dayMax;
    public int month;
    public int year;

    public TextMeshProUGUI dayDisplay;
    public TextMeshProUGUI monthDisplay;
    public TextMeshProUGUI yearDisplay;

    public bool isPaused;


    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        day = 1;
        month = 1;
        year = 2010;

        SetUI();
    }


    // Update is called once per frame
    void Update()
    {
        if(isPaused == false)
        {
            time += Time.deltaTime;
        }


        if(time >= 48)
        {
            hour++;
            time = 0;
        }

        if(hour >= 24)
        {
            day++;
            hour = 0;
            SetUI();
        }

        MonthCheck(month);
        if(day > dayMax)
        {
            month++;
            day = 1;
            SetUI();
        }

        if(month >= 13)
        {
            year++;
            month = 1;
            SetUI();
        }
    }

    private void SetUI()
    {
        dayDisplay.text = day.ToString() + " /";
        monthDisplay.text = month.ToString() + " /";
        yearDisplay.text = year.ToString();
    }


    public void MonthCheck(int month)
    {
        switch(month)
        {
            case 1:
                {
                    dayMax = 31;
                    break;
                }
            case 2:
                {
                    if(year % 4 == 0)
                    {
                        dayMax = 29;
                    }

                    else
                    {
                        dayMax = 28;
                    }

                    break;
                }
            case 3:
                {
                    dayMax = 31;
                    break;
                }
            case 4:
                {
                    dayMax = 31;
                    break;
                }
            case 5:
                {
                    dayMax = 30;
                    break;
                }
            case 6:
                {
                    dayMax = 31;
                    break;
                }
            case 7:
                {
                    dayMax = 30;
                    break;
                }
            case 8:
                {
                    dayMax = 31;
                    break;
                }
            case 9:
                {
                    dayMax = 30;
                    break;
                }
            case 10:
                {
                    dayMax = 31;
                    break;
                }
            case 11:
                {
                    dayMax = 30;
                    break;
                }
            case 12:
                {
                    dayMax = 31;
                    break;
                }
            default:
                {
                    dayMax = 31;
                    break;
                }



        }
    }
}
