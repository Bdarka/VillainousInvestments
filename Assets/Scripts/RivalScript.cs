using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RivalScript : MonoBehaviour
{
    public int rivalStartingMoney;
    public int rivalMoney;
    public int rivalIncome;

    public float actionTimer;

    public float randomX, randomZ;
    public Vector3 rolledPositon;

    public BuildingSystem buildingSystem;
    public SituationSystem situationSystem;

    public PlaceableObject buildingPrefab;
    public PlaceableObject createdBuilding;
    

    // Start is called before the first frame update
    void Start()
    {
        buildingSystem = GameObject.Find("Grid").GetComponent<BuildingSystem>();
        situationSystem = GameObject.Find("GameManager").GetComponent<SituationSystem>();

        rivalMoney = rivalStartingMoney;

        RivalPlaceBuilding();

        actionTimer = Random.Range(45, 500);
    }

    // Update is called once per frame
    void Update()
    {
        actionTimer -= Time.deltaTime;

        if (actionTimer < 0)
        {
            RivalPlaceBuilding();
            actionTimer = Random.Range(45, 500);
        }
    }

    public void RivalPlaceBuilding()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        createdBuilding = Instantiate(buildingPrefab, startPos, Quaternion.identity);
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

        buildingSystem.objectToPlace = null;

        situationSystem.DisplayEventWindow("Accept");
        situationSystem.RivalPlacedBuilding();    
    }
}

