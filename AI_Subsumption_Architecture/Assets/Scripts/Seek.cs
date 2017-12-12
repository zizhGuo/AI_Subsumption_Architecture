using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public byte motorCode;

    public GameObject chickenPooler;
    public GameObject wolf;

    NewObjectPoolerScript chickenPoolerScript;
    public Wander wanderScript;
    public Avoid avoidScript;
    public Attack attackScript;
    public Flee fleescript;
    public Health healthScript;

    public int targetNumber;
    public bool isSeeking;
    private float currentTime;

    public Vector3 seekDirection;
    void Start()
    {
        motorCode = 0;

        chickenPoolerScript = chickenPooler.GetComponent<NewObjectPoolerScript>();
        wanderScript = wolf.GetComponent<Wander>();
        avoidScript = wolf.GetComponent<Avoid>();
        attackScript = wolf.GetComponent<Attack>();
        healthScript = wolf.GetComponent<Health>();
        fleescript = wolf.GetComponent<Flee>();

        targetNumber = 0;

        isSeeking = false;
    }

    void Update()
    {
        if (fleescript.isFleeing)
        {
            isSeeking = false;
        }
        if (attackScript.isAttacking)
        {
            isSeeking = false;
        }

        for (int i = 0; i < chickenPoolerScript.pooledAmount; i++)
        {

            if (chickenPoolerScript.pooledObjects[i].activeInHierarchy && Vector3.Distance(chickenPoolerScript.pooledObjects[i].transform.position, transform.position) < 2)
            {
                targetNumber = i;
                seekDirection = chickenPoolerScript.pooledObjects[i].transform.position - transform.position;
                chickenPoolerScript.transform.LookAt(chickenPoolerScript.pooledObjects[i].transform.position);
                if (!attackScript.isAttacking && !fleescript.isFleeing)
                {
                    isSeeking = true;
                }
                else isSeeking = false;
                break;
            }
            if (chickenPoolerScript.pooledObjects[i] == null) return;

        }
        if (!avoidScript.isAvoiding)
        {
            if (isSeeking) // Condition!!!!!
            {
                motorCode = 1;
                wanderScript.isWandering = false;


                if (Vector3.Distance(chickenPoolerScript.pooledObjects[targetNumber].transform.position, transform.position) < 0.2 && !wanderScript.isWandering)
                {
                    //currentTime = Time.time;
                    //if (Time.time - currentTime > 1)
                    //{
                    chickenPoolerScript.pooledObjects[targetNumber].SetActive(false);
                    //currentTime = Time.time;
                    isSeeking = false;
                    wanderScript.isWandering = true;
                    motorCode = 0;
                    //}
                }
            }
            else
            {
                motorCode = 0;
            }
        }
    }
}
