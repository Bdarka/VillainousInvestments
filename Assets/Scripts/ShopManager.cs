using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public GameManager GameManager;
    public BuildingSystem BuildingSystem;
    public GameObject MessageWindow;

    public GameObject shopView;
    public GameObject shopButton;

    public void Start()
    {
        shopButton.SetActive(true);
        shopView.SetActive(false);
    }

    public void BuyBuilding()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        int itemID = ButtonRef.GetComponent<ButtonInfo>().BuildingID;

        BuildingType building = BuildingSystem.prefabBuilds[itemID].GetComponent<BuildingType>();

        if (building == null)
        {
            return;
        }


        if(GameManager.playerMoney >= building.BuildingCost)
        {
            BuildingSystem.InitializeWithObject(BuildingSystem.prefabBuilds[itemID]);
        }
        else
        {
            return;
        }

        shopView.SetActive(false);
        shopButton.SetActive(true);
    }

    public void OpenShop()
    {
        if(MessageWindow.activeSelf == false)
        {
            shopView.SetActive(true);
            shopButton.SetActive(false);
        }
    }

}
