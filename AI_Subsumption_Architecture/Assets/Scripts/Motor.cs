using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
    public GameObject wolf;

    public Wander wanderScript;
    public Avoid avoidScript;
    public Seek seekScript;
    public Attack attackScript;
    public Flee fleeScript;
    public Recover recoverScript;
    public Health healthScript;

    public int motorCodes; // Avoid(1), Wander(1), Seek(1), Attack(1), Flee(1), Recover(1) = 00111111 default: 00 01 00 00 = 0b00010000
    private float currentTime;

    private void Awake()
    {
        wanderScript = wolf.GetComponent<Wander>();
        avoidScript = wolf.GetComponent<Avoid>();
        seekScript = wolf.GetComponent<Seek>();
        attackScript = wolf.GetComponent<Attack>();
        fleeScript = wolf.GetComponent<Flee>();
        recoverScript = wolf.GetComponent<Recover>();
        healthScript = wolf.GetComponent<Health>();
        currentTime = Time.time;
    }
    void Start() // Initialization
    {
        //motorCodes = GetMotorCodes(wanderScript, avoidScript, seekScript, attackScript, fleeScript, recoverScript);
    }
    int GetMotorCodes(Avoid a, Wander b, Seek c, Attack d, Flee e, Recover f)
    {
        motorCodes = f.motorCode + e.motorCode * 2 + d.motorCode * 4 + c.motorCode * 8 + b.motorCode * 16 + a.motorCode * 32;
        return motorCodes;
    }
    void DebugMotorCodes(Avoid a, Wander b, Seek c, Attack d, Flee e, Recover f)
    {
        var A = a.motorCode;    
        var B = b.motorCode ;
        var C = c.motorCode;
        var D = d.motorCode;
        var E = e.motorCode;
        var F = f.motorCode;
        Debug.Log("MotorCodes: " + A + B + C + D + E + F);
        
    }
    private void FixedUpdate()
    {

        motorCodes = GetMotorCodes(avoidScript, wanderScript, seekScript, attackScript, fleeScript, recoverScript);

        if (Time.time - currentTime > 1)
        {
            DebugMotorCodes(avoidScript, wanderScript, seekScript, attackScript, fleeScript, recoverScript);
            Debug.Log("MotorCodes = " + motorCodes);
            currentTime = Time.time;
        }

        if (motorCodes == 32) // avoid
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidScript.reflectVec), 0.2f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + avoidScript.dirrection.x * 0.01f, transform.position.y, transform.position.z + avoidScript.dirrection.z * 0.01f);
        }
        //if (motorCodes == 34) // avoid
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(avoidScript.reflectVec), 0.2f * Time.deltaTime);
        //    transform.position = new Vector3(transform.position.x + avoidScript.dirrection.x * 0.01f, transform.position.y, transform.position.z + avoidScript.dirrection.z * 0.01f);
        //}

        if (motorCodes == 16) //wander
        {
            transform.position = new Vector3(transform.position.x + wanderScript._velocity.x * Time.deltaTime, 0, transform.position.z + wanderScript._velocity.z * Time.deltaTime);

            if (wanderScript.rotateAngle < 180)
            {
                transform.Rotate(Vector3.up, -Mathf.Acos((wanderScript.force * wanderScript.force + 0.5625f) / (2.5f * (float)wanderScript.force)));
            }
            else
            {
                transform.Rotate(Vector3.up, Mathf.Acos((wanderScript.force * wanderScript.force + 0.5625f) / (2.5f * (float)wanderScript.force)));
            }
        }
        if (motorCodes == 8) // seek
        {
            transform.position = new Vector3(transform.position.x + Vector3.Normalize(seekScript.seekDirection).x * Time.deltaTime, 0, transform.position.z + Vector3.Normalize(seekScript.seekDirection).z * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(seekScript.seekDirection), 2f * Time.deltaTime);
        }

        if (motorCodes == 4) // attack
        {
            transform.position = new Vector3(transform.position.x + Vector3.Normalize(attackScript.dir).x * Time.deltaTime, 0, transform.position.z + Vector3.Normalize(attackScript.dir).z * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(attackScript.dir), 2f * Time.deltaTime);
        }
        if (motorCodes == 2)// flee
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(fleeScript.fleeDir), 2f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + Vector3.Normalize(fleeScript.fleeDir).x * Time.deltaTime, 0, transform.position.z + Vector3.Normalize(fleeScript.fleeDir).z * Time.deltaTime);

        }
    }
}
