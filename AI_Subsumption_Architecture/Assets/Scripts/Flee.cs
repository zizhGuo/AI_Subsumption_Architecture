using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public byte motorCode;

    public GameObject wolf;

    private Health healthScript;
    private Attack attackScript;
    public Vector3 fleeDir;

    public bool isFleeing;
    void Start()
    {
        motorCode = 0;

        healthScript = wolf.GetComponent<Health>();
        attackScript = wolf.GetComponent<Attack>();
        isFleeing = false;
    }

    void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        if (healthScript.currentHealth < 30) // HP
        {
            isFleeing = true;
        }
        if (isFleeing)
        {
            motorCode = 1;
            attackScript.isAttacking = false;
            fleeDir = new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z);
            //测试专用
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fleeDir), 2f * Time.deltaTime);
            //transform.position = new Vector3(transform.position.x + Vector3.Normalize(fleeDir).x * Time.deltaTime, 0, transform.position.z + Vector3.Normalize(fleeDir).z * Time.deltaTime);

        }
    }
    public void OnTriggerExit(Collider other)
    {
        isFleeing = false;
        motorCode = 0;
    }
}
