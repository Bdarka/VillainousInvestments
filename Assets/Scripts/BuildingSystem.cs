using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

// Used 3D Grid Building System Tutorial from Tamara Makes Games 
// https://youtu.be/rKp9fWvmIww?si=xXxy6pbVzfV0GHvi

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;

    public GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap MainTileMap;
    [SerializeField] private TileBase whiteTile;

    // Slight deviation from tutorial. I want the project to be more scaleable
    public List<GameObject> prefabBuilds = new List<GameObject>();

   // public Dictionary<PlaceableObject, Button> BuildingButtonPairs = new Dictionary<PlaceableObject, Button>();
    public PlaceableObject[] placeableObjects;
    public Button[] Buttons;

    public PlaceableObject objectToPlace;
    private int buildingCount;
    public GameObject buildingParent;



    #region Unity Methods 

    private void Awake()
    {
        current = this;

        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.A) && objectToPlace == null)
        {
            InitializeWithObject(prefabBuilds[0]);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            InitializeWithObject(prefabBuilds[1]);
        }
        */
        if(objectToPlace == null)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            objectToPlace.Rotate();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(CanBePlaced(objectToPlace) && objectToPlace.myBuildingType.BuildingCost <= GameManager.instance.playerMoney)
            {
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
                objectToPlace.transform.parent = buildingParent.transform;
                objectToPlace = null;
            }

            else
            {
                Destroy(objectToPlace.gameObject);
            }

        }

        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    #endregion


    #region Utils

    // Preffered way to get mouse input in 3D games
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }

    }

    // this is to keep track of the building sizes +
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        // sounds like my tutorial was having a hard time with the built in Unity function, so they did this instead
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;

        foreach(Vector3Int v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }


    /*
    public void DictionarySetup()
    {
        // Pairing each building with its respective button
        for(int  i = 0; i < placeableObjects.Length; i++)
        {
            BuildingButtonPairs.Add(placeableObjects[i], Buttons[i]);
        }
    }
    */
    #endregion

    #region Building Placement

    /* trying to make buttons determine the building. Will revist later
    public void WhichBuild(Button button)
    {
        BuildingButtonPairs.ContainsKey
    }
    */
    public void InitializeWithObject(GameObject prefab)
    {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);

        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        obj.gameObject.name = prefab.name + buildingCount;

        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();
        buildingCount++;
    }

    public bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x +1, area.size.y +1, area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, MainTileMap);

        foreach(TileBase tile in baseArray)
        {
            if (tile != null)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        MainTileMap.BoxFill(start, whiteTile, start.x, start.y, 
                            start.x + size.x, start.y + size.y);
    }


    public void FreeArea(Vector3Int start, Vector3Int size)
    {
        MainTileMap.BoxFill(start, null, start.x, start.y,
                            start.x + size.x, start.y + size.y);
    }
    #endregion
}
