using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Wander : MonoBehaviour
{
    public byte motorCode;

    public GameObject wolf;
    public GameObject center;
    public GameObject pointer;

    public Avoid avoidScript;
    public Seek seekScript;
    public Attack attackScript;
    public Flee fleeScript;

    public System.Random r;
    public System.Random r2;

    private float currentTime;
    public float force;
    public float rotateAngle;
    private float rotateAngle2;
    private int rotateDirection;
    public bool isWandering;

    private Vector3 V1;
    private Vector3 V2;
    public Vector3 _acceleration;
    public Vector3 _velocity;

    void Start()
    {
        motorCode = 1;
        avoidScript = wolf.GetComponent<Avoid>();
        seekScript = wolf.GetComponent<Seek>();
        attackScript = wolf.GetComponent<Attack>();
        fleeScript = wolf.GetComponent<Flee>();
        r = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);
        r2 = new System.Random((int)DateTime.Now.Ticks % int.MaxValue);
        V1 = transform.position - center.transform.position;
        rotateDirection = r2.Next(0, 99);
        _velocity = new Vector3(0, 0f, 0);
        isWandering = true;
    }

    void Update()
    {
        if (!avoidScript.isAvoiding) isWandering = true;
        if (seekScript.isSeeking) isWandering = false;
        if (attackScript.isAttacking) isWandering = false;
        if (fleeScript.isFleeing) isWandering = false;
        if (isWandering) // Condition!!!!!
        {
            //if (!avoidScript.isAvoiding)
            motorCode = 1;
            if (Time.time - currentTime > 3)
            {
                rotateAngle = r.Next(120, 240);
                rotateDirection = r2.Next(0, 99);
                currentTime = Time.time;
            }
            if (rotateDirection < 50)
            {
                rotateAngle = rotateAngle + 0.1f;
            }
            else
            {
                rotateAngle = rotateAngle - 0.1f;
            }
            if (rotateAngle > 360)
            {
                rotateAngle = 0;
            }

            force = Mathf.Sqrt((float)(2.5625 - 2.5 * Mathf.Cos(rotateAngle * Mathf.Deg2Rad)));
            pointer.GetComponent<Transform>().localPosition = new Vector3(-Mathf.Sin(rotateAngle * Mathf.Deg2Rad), center.transform.position.y, 1.25f - Mathf.Cos(rotateAngle * Mathf.Deg2Rad));
            V1 = Vector3.Normalize(pointer.transform.position - transform.position);
            _acceleration = new Vector3(V1.x * force * force * 10, 0f, V1.z * force * force * 10);
            _velocity = Vector3.ClampMagnitude(new Vector3(_velocity.x + _acceleration.x * Time.deltaTime, 0f, _velocity.z + _acceleration.z * Time.deltaTime), 0.8f);

            //单项功能测试
            //transform.position = new Vector3(transform.position.x + _velocity.x * Time.deltaTime, 0, transform.position.z + _velocity.z * Time.deltaTime);

            //if (rotateAngle < 180)
            //{
            //    transform.Rotate(Vector3.up, -Mathf.Acos((force * force + 0.5625f) / (2.5f * (float)force)));
            //}
            //else
            //{
            //    transform.Rotate(Vector3.up, Mathf.Acos((force * force + 0.5625f) / (2.5f * (float)force)));
            //}
        }
        else
        {
            motorCode = 0;
        }





        if (transform.position.x > 5.8f)
        {
            transform.position = new Vector3(-5.8f, 0, transform.position.z);
        }
        if (transform.position.x < -5.8f)
        {
            transform.position = new Vector3(5.8f, 0, transform.position.z);
        }
        if (transform.position.z > 5.8f)
        {
            transform.position = new Vector3(transform.position.z, 0, -5.8f);
        }
        if (transform.position.z < -5.8f)
        {
            transform.position = new Vector3(transform.position.z, 0, 5.8f);
        }

    }
}
