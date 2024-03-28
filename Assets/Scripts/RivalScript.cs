using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RivalScript : MonoBehaviour
{
    public int rivalStartingMoney;
    public int rivalMoney;
    public int rivalIncome;

    private float actionTimer;

    public float randomX, randomZ;
    public Vector3 rolledPositon;

    private GameObject findBuildingSystem;
    public BuildingSystem buildingSystem;

    public PlaceableObject buildingPrefab;
    public PlaceableObject createdBuilding;

    // Start is called before the first frame update
    void Start()
    {
        findBuildingSystem = GameObject.Find("Grid");
        buildingSystem = findBuildingSystem.GetComponent<BuildingSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RivalPlaceBuilding()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        createdBuilding = Instantiate(buildingPrefab, startPos, Quaternion.identity);
        createdBuilding.playersBuilding = false;
        buildingSystem.CanBePlaced(createdBuilding);

        while (buildingSystem.CanBePlaced(createdBuilding) == false)
        {
            randomX = Random.Range(-28, 28);
            randomZ = Random.Range(-28, 28);
            rolledPositon = new Vector3(randomX, 0, randomZ);
            createdBuilding.gameObject.transform.position = new Vector3(randomX, 0, randomZ);
            buildingSystem.CanBePlaced(createdBuilding);
        }


    }
}

