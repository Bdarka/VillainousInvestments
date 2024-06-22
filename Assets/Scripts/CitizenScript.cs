using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CitizenScript : MonoBehaviour
{
    public GameObject Body;

    public GameObject Home;
    public GameObject Work;
    public Transform currentDestination;

    public float currentMoney;
    public float income;
    public float rent;
    public float expenses;

    public int happinessLevel;
    public int energyLevel;
    public float moveSpeed;

    private NavMeshAgent navMeshAgent;

    public TimeManagerScript timeManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        // Go through player's available housing and move into one. Add that to rent


        // Pick workplace

        // Set Colors for NPC
    }

    // Update is called once per frame
    void Update()
    {
        if(timeManagerScript.time/2 > 8 && timeManagerScript.time/2 < 17)
        {
            MoveToDestination(Work.transform);
        }

        if(timeManagerScript.time/2 < 8 || timeManagerScript.time/2 > 17)
        {
            MoveToDestination(Home.transform);
        }
    }

    public void MoveToDestination(Transform destination) 
    {
        if(navMeshAgent.remainingDistance < 0.5f)
        {
            navMeshAgent.SetDestination(destination.position);
        }
        else
        {
            Body.SetActive(false);
        }
    }
}