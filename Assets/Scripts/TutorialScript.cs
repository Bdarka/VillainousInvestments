using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public GameManager gameManager;
    public SituationSystem situationSystem;

    public int tutorialCount;

    public GameObject MessageWindow;
    public TextMeshProUGUI EventName;
    public TextMeshProUGUI EventText;

    // Start is called before the first frame update
    void Start()
    {
        Tutorial1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region

    public void Tutorial1()
    {
        situationSystem.DisplayEventWindow();
        EventName.text = "Tutorial #1";
        EventText.text = "Welcome to Villainous Investments!";
    }

    #endregion
}
