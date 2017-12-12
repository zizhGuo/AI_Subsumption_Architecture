using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recover : MonoBehaviour
{
    public byte motorCode;
    public bool isRecover;

    // Use this for initialization
    void Start()
    {
        motorCode = 0;
        isRecover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRecover)
        {

            motorCode = 1;
        }
        else motorCode = 0;
    }
}
