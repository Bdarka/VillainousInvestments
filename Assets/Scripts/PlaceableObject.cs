using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed {  get; private set; }
    public Vector3Int Size { get; private set; }
    [HideInInspector]public Vector3[] Vertices;

    public BoxCollider[] buildingChecks;

    public BuildingType myBuildingType;

    public bool playersBuilding;

    public List<BuildingType> adjacentBuildings = new List<BuildingType>();
    private void GetColliderVertexPositionsLocal()
    {
        // ok I'm sick but from what I understand this is getting the center of the box collider, 
        // then trying to find the coordinates for each corner
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        Vertices = new Vector3[4];
        Vertices[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        Vertices[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        Vertices[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x), Mathf.Abs((vertices[0] - vertices[3]).y), 1);
                
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Awake()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();

        myBuildingType = GetComponent<BuildingType>();
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0, 90, 0));
        Size = new Vector3Int(Size.y, Size.x, 1);

        Vector3[] vertices = new Vector3[Vertices.Length];

        for(int i = 0;i < vertices.Length;i++)
        {
            vertices[i] = Vertices[(i + 1) % Vertices.Length];
        }

        Vertices = vertices;
    }

    public virtual void Place()
    {

        ObjectDrag drag = gameObject.GetComponent<ObjectDrag>();
        if (drag != null)
        {
            Destroy(drag);
        }


        Placed = true;

        // invoke placement events here 

        CheckSurroundings();
        
        GameManager.instance.BuildingTracking(myBuildingType, this);
    }

    public void CheckSurroundings()
    {        

        for (int i = 0; i < buildingChecks.Length; i++)
        {
            Physics.SyncTransforms();

            Vector3 size = buildingChecks[i].transform.TransformVector(buildingChecks[i].transform.position);
            size.x = Mathf.Abs(size.x);
            size.y = Mathf.Abs(size.y);
            size.z = Mathf.Abs(size.z);

            Collider[] col = Physics.OverlapBox(buildingChecks[i].transform.position, size / 3, Quaternion.identity);

            foreach (Collider c in col)
            {
                BuildingType bt = c.GetComponent<BuildingType>();

               // Debug.Log(myBuildingType + " is my building");

                if (bt != null)
                {
                   // Debug.Log(c.gameObject.name + " is the other building");

                    switch (bt.buildingName)
                    {
                        case BuildingType.BuildingName.Office:
                            {
                                if (myBuildingType.buildingName == BuildingType.BuildingName.Office 
                                    && adjacentBuildings.Contains(bt) == false)
                                {

                                    if (bt.gameObject == this.gameObject)
                                    {
                                        break;
                                    }
                                    myBuildingType.BuildingPayOut += 10;
                                    myBuildingType.BuildingLandWorth += 10;

                                    //Debug.Log(buildingChecks[i].gameObject.name + " has made " + gameObject.name + "Pay out now: " + myBuildingType.BuildingPayOut);

                                    bt.BuildingPayOut += 10;
                                    bt.BuildingLandWorth += 10;

                                    adjacentBuildings.Add(bt);

                                   // Debug.Log(bt.gameObject.name + " now has a pay out of:" + bt.BuildingPayOut + " because of " + buildingChecks[i]);
                                    bt = null;
                                }

                                else if (myBuildingType.buildingName == BuildingType.BuildingName.Swamp)
                                {
                                    bt.BuildingPayOut -= 10;
                                    bt.BuildingLandWorth -= 5;

                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }

                                else if(myBuildingType.buildingName == BuildingType.BuildingName.RivalOffice)
                                {
                                    myBuildingType.BuildingPayOut += 10;
                                    myBuildingType.BuildingLandWorth += 10;

                                    bt.BuildingPayOut += 10;
                                    bt.BuildingLandWorth += 10;
                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }

                                break;
                            }
                        case BuildingType.BuildingName.Swamp:
                            {
                                if (myBuildingType.buildingName == BuildingType.BuildingName.Swamp 
                                    && adjacentBuildings.Contains(bt) == false)
                                {
                                    if (bt.gameObject == this.gameObject)
                                    {
                                        break;
                                    }

                                    myBuildingType.BuildingPayOut -= 10;
                                    myBuildingType.BuildingLandWorth -= 10;

                                    bt.BuildingPayOut -= 10;
                                    bt.BuildingLandWorth -= 10;
                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }
                                
                                else if(myBuildingType.buildingName == BuildingType.BuildingName.Office 
                                    && adjacentBuildings.Contains(bt) == false)
                                {
                                    myBuildingType.BuildingPayOut -= 20;
                                    myBuildingType.BuildingLandWorth -= 10;

                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }

                                else if(myBuildingType.buildingName == BuildingType.BuildingName.RivalOffice)
                                {
                                    myBuildingType.BuildingPayOut -= 20;
                                    myBuildingType.BuildingLandWorth -= 10;

                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }

                                break;
                            }
                        case BuildingType.BuildingName.RivalOffice:
                            {
                                if (myBuildingType.buildingName == BuildingType.BuildingName.RivalOffice 
                                    && adjacentBuildings.Contains(bt) == false)
                                {
                                    if (bt.gameObject == this.gameObject)
                                    {
                                        break;
                                    }

                                    myBuildingType.BuildingPayOut += 10;
                                    myBuildingType.BuildingLandWorth += 10;

                                    bt.BuildingPayOut += 10;
                                    bt.BuildingLandWorth += 10;
                                    adjacentBuildings.Add(bt);
                                    bt = null;

                                }

                                else if(myBuildingType.buildingName == BuildingType.BuildingName.Office 
                                    && adjacentBuildings.Contains(bt) == false)
                                {
                                    myBuildingType.BuildingPayOut += 10;
                                    myBuildingType.BuildingLandWorth += 10;

                                    bt.BuildingPayOut += 10;
                                    bt.BuildingLandWorth += 10;
                                    adjacentBuildings.Add(bt);
                                    bt = null;
                                }

                                else if(myBuildingType.buildingName == BuildingType.BuildingName.Swamp
                                    && adjacentBuildings.Contains(bt) == false)
                                {
                                    bt.BuildingPayOut -= 15;
                                    bt.BuildingLandWorth -= 5;
                                    adjacentBuildings.Add(bt);
                                    bt = null;

                                }

                                break;
                            }

                    }
                }
            }

            Destroy(buildingChecks[i].gameObject);
        }
        buildingChecks = null;
        Destroy(GetComponent<Rigidbody>());
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(buildingChecks != null)
        {
            foreach (Collider box in buildingChecks)
            {
                Gizmos.DrawCube(box.transform.position, box.GetComponent<BoxCollider>().size);
            }
        }
    }
}
