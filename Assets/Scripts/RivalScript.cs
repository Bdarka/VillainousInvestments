using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class RivalScript : MonoBehaviour
{
    public int rivalStartingMoney;
    public int rivalMoney;
    public int rivalIncome;
    public List<GameObject> rivalBuildings = new List<GameObject>();

    public float actionTimer;

    public float randomX, randomZ;
    public Vector3 rolledPositon;

    public BuildingSystem buildingSystem;
    public SituationSystem situationSystem;

    public PlaceableObject buildingPrefab;
    public PlaceableObject orphanagePrefab;
    public PlaceableObject createdBuilding;
    

    // Start is called before the first frame update
    void Start()
    {
        buildingSystem = GameObject.Find("Grid").GetComponent<BuildingSystem>();
        situationSystem = GameObject.Find("GameManager").GetComponent<SituationSystem>();

        rivalMoney = rivalStartingMoney;

        RivalPlaceBuilding(buildingPrefab);

        actionTimer = Random.Range(45, 500);
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer -= Time.deltaTime;

        if (actionTimer < 0)
        {
            RivalPlaceBuilding(buildingPrefab);
            actionTimer = Random.Range(15, 150);
        }

        if(rivalBuildings.Count > 0)
        {
            CalculateLandWorth();
        }

        if(rivalMoney <= 0)
        {
            situationSystem.DisplayEventWindow("Accept");
            situationSystem.EventName.text = "Rival Defeated";
            situationSystem.EventText.text = "You bankrupted your rival! Try to earn $10000";
        }
    }

    #region Rival Money Functions

    private void CalculateLandWorth()
    {
        foreach(GameObject obj in rivalBuildings)
        {
            if (obj != null)
            {
                BuildingType bt = obj.GetComponent<BuildingType>();
                PlaceableObject po = obj.GetComponent<PlaceableObject>();

                if(bt.BuildingLandWorth <= 0)
                {
                    rivalMoney += bt.BuildingSellPrice;

                    Vector3Int start = buildingSystem.gridLayout.WorldToCell(po.GetStartPosition());

                    buildingSystem.FreeArea(start, po.Size); 

                    Destroy(obj.gameObject);
                }
            }
        }
    }

    public int CalculateIncome()
    {
        rivalIncome = 0;

        foreach (GameObject obj in rivalBuildings)
        {
            if (obj != null)
            {   
                BuildingType bt = obj.GetComponent<BuildingType>();

                rivalIncome += bt.BuildingPayOut;
            }
        }
        
        return rivalIncome;
    }

    #endregion

    public void RivalPlaceBuilding(PlaceableObject prefab)
    {
        //Vector3 startPos = new Vector3(0, 0, 0);

        Vector3 startPos = buildingSystem.SnapCoordinateToGrid(Vector3.zero);

        createdBuilding = Instantiate(prefab, startPos, Quaternion.identity);
        createdBuilding.gameObject.transform.parent = this.gameObject.transform;

        buildingSystem.objectToPlace = createdBuilding;
        buildingSystem.CanBePlaced(createdBuilding);

        Debug.Log(createdBuilding.name);

        while (buildingSystem.CanBePlaced(createdBuilding) == false)
        {
            randomX = Random.Range(-28, 28);
            randomZ = Random.Range(-28, 28);
            rolledPositon = new Vector3(randomX, 0, randomZ);
            createdBuilding.gameObject.transform.position = new Vector3(randomX, 0, randomZ);
            buildingSystem.CanBePlaced(createdBuilding);
        }

        createdBuilding.Place();
        Vector3Int start = buildingSystem.gridLayout.WorldToCell(createdBuilding.GetStartPosition());
        buildingSystem.TakeArea(start, createdBuilding.Size);

        rivalBuildings.Add(createdBuilding.gameObject);

        buildingSystem.objectToPlace = null;

        situationSystem.DisplayEventWindow("Accept");
        situationSystem.RivalPlacedBuilding();    
    }
}

