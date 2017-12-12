using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public byte motorCode;

    public GameObject wolf;
    public GameObject chickenPooler;
    private GameObject tempObj;

    private Health healthScript;
    private NewObjectPoolerScript chickenPoolerScript;
    private Seek seekScript;
    private Flee fleeScript;
    private Wander wanderScript;

    private float currentTime;
    public bool isAttacking;

    public Vector3 dir;
    void Start()
    {
        motorCode = 0;
        isAttacking = false;
        healthScript = wolf.GetComponent<Health>();
        chickenPoolerScript = chickenPooler.GetComponent<NewObjectPoolerScript>();
        seekScript = wolf.GetComponent<Seek>();
        fleeScript = wolf.GetComponent<Flee>();
        wanderScript = wolf.GetComponent<Wander>();
        currentTime = Time.time;
        tempObj = null;
    }
    public void Update()
    {
        if (fleeScript.isFleeing)
        {
            isAttacking = false;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wolf" && tempObj == null) // Condition!!!!!
        {
            tempObj = other.gameObject;
        }

        if (tempObj.tag == "Wolf")
        {
            for (int i = 0; i < chickenPoolerScript.pooledObjects.Count; i++)
            {
                if (chickenPoolerScript.pooledObjects[seekScript.targetNumber].activeInHierarchy)
                {
                    if (Vector3.Distance(tempObj.transform.position, chickenPoolerScript.pooledObjects[seekScript.targetNumber].transform.position) < 3 && !fleeScript.isFleeing)
                    {
                        isAttacking = true;
                        ///Motor Part below:
                        //进攻 朝敌人前进
                        dir = new Vector3(tempObj.transform.position.x - transform.position.x, 0, tempObj.transform.position.z - transform.position.z);
                        //////仅限测试用代码
                        //transform.position = new Vector3(transform.position.x + Vector3.Normalize(dir).x * Time.deltaTime * 0.1f, 0, transform.position.z + Vector3.Normalize(dir).z * 0.1f * Time.deltaTime);
                        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 2f * Time.deltaTime);
                        //距离内伤害减血
                        if (Vector3.Distance(transform.position, tempObj.transform.position) < 0.3f)
                        {
                            if (Time.time - currentTime > 3)
                            {
                                healthScript.TakeDamage(10);
                                currentTime = Time.time;
                            }
                        }
                    }
                    Debug.Log("On Trigger enter");
                }
            }
        }
        if (isAttacking)
        {
            motorCode = 1;
            seekScript.isSeeking = false;
            wanderScript.isWandering = false;
        }
        else
        {
            motorCode = 0;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        isAttacking = false;
        motorCode = 0;
        Debug.Log("Exit");
    }
}
