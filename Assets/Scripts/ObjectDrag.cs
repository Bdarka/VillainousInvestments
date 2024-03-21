using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    
    private Vector3 offset;


    private void OnMouseDown()
    {
        // trying to find the difference between the object's position and the mouse cursor
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();

    }

    private void OnMouseDrag()
    {
        Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }
}
