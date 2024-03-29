using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuildingType : MonoBehaviour
{
    public enum BuildingName
    {
        Office,
        Swamp,
        Warehouse,
        RivalOffice
    }

    public int BuildingCost;
    public int BuildingSellPrice;
    public int BuildingPayOut;
    public int BuildingLandWorth;
    public BuildingName buildingName;
   


    /*
     * "Low Poly Swamp" (https://skfb.ly/6WRKO) by smithb6 is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).
     */
}
