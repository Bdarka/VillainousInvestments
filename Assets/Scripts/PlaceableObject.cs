using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool Placed {  get; private set; }
    public Vector3Int Size { get; private set; }
    [HideInInspector]public Vector3[] Vertices;

    public Rigidbody rb;
    public BoxCollider buildingCheck;

    public BuildingType buildingType;

    public bool playersBuilding;


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

        buildingCheck = GetComponentInChildren<BoxCollider>();
        rb = GetComponentInChildren<Rigidbody>();
        buildingType = GetComponent<BuildingType>();
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

        
        
        GameManager.instance.BuildingTracking(buildingType, this);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Collider[] col = Physics.OverlapBox(this.transform.position, collision.transform.position);

        foreach(Collider c in col)
        {
            BuildingType bt = c.GetComponent<BuildingType>();
            
            if(bt != null)
            {
                switch(bt.BuildingName)
                {
                    case BuildingType.BuildingName.Office:
                        {
                            break;
                        }
                    case BuildingType.BuildingName.Swamp:
                        {
                            break;
                        }
                    case BuildingType.BuildingName.RivalOffice:
                        {
                            break;
                        }

                }

            }
        }

        buildingCheck.isTrigger = true;
    }
}
