using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    public int BuildingID;
    public TextMeshProUGUI priceTxt;
    public TextMeshProUGUI buildingTxt;

    public GameObject ShopManager;
    public BuildingSystem BS;

    public void Start()
    {
        priceTxt.text = BS.prefabBuilds[BuildingID].GetComponent<BuildingType>().BuildingCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
