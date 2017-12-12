using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Avoid : MonoBehaviour
{
    public byte motorCode;

    public GameObject rockPooler;
    public GameObject wolf;
    public Wander wanderScript;
    public Seek seekScript;
    public Attack attackScript;
    public Flee fleeScript;
    public Recover recoverScript;

    private NewObjectPoolerScript rockPoolerScript;

    public System.Random r;
    private float currentTime;
    private float minimumDistToAvoid;
    public bool isAvoiding;
    public int layerMask;

    public Vector3 dirrection;
    public Vector3 reflectVec;
    public RaycastHit hit1;
    void Start()
    {
        motorCode = 0;
        rockPoolerScript = rockPooler.GetComponent<NewObjectPoolerScript>();
        wanderScript = wolf.GetComponent<Wander>();
        seekScript = wolf.GetComponent<Seek>();
        attackScript = wolf.GetComponent<Attack>();
        fleeScript = wolf.GetComponent<Flee>();
        recoverScript = wolf.GetComponent<Recover>();

        currentTime = Time.time;
        r = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);
        minimumDistToAvoid = 1f;
        layerMask = 1 << 9;
        isAvoiding = false;
        
    }
    void Update()
    {

        if (Time.time - currentTime > 3)
        {
            r = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);

            if (r.Next(0, 100) < 50) dirrection = Vector3.left;
            else dirrection = Vector3.right;
        }

        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask);
        //Debug.DrawRay(transform.position, transform.forward, Color.red, 10f);
        hit1 = hit;
        if (hit.point != new Vector3(0,0,0))
        {
            Debug.DrawLine(transform.position, hit.point, Color.blue, 10f);
        }       
        dirrection = hit.point - transform.position; //   reverse?
        reflectVec = Vector3.Reflect(dirrection, hit.normal);
        if (hit.collider != null) // Condition!!!!!
        {
            Debug.Log("you collider!");
            isAvoiding = true;
        }
        else isAvoiding = false;
        if (isAvoiding) // Condition!!!!!
        {
            motorCode = 1;
            ///仅限测试用
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(reflectVec), 0.2f * Time.deltaTime);
            //transform.position = new Vector3(transform.position.x + dirrection.x * 0.01f, transform.position.y, transform.position.z + dirrection.z * 0.01f);

            wanderScript.isWandering = false;
            seekScript.isSeeking = false;
            attackScript.isAttacking = false;
            fleeScript.isFleeing = false;
            recoverScript.isRecover = false;
        }
        else motorCode = 0;
    }
}
