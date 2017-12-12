using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    float xAxisValue;
    float zAxisValue;
    float yAxisValue;

    void Start()
    {

    }
    void Update()
    {
        xAxisValue = Input.GetAxis("Horizontal");
        zAxisValue = Input.GetAxis("Vertical");
        //yAxisValue = Input.GetAxis("Mouse ScrollWheel");
        if (Camera.current != null)
        {
           // Camera.current.transaform.Translate(new Vector3(xAxisValue, 0.0f, zAxisValue));
            Camera.current.transform.position = new Vector3(transform.position.x + xAxisValue, transform.position.y, transform.position.z + zAxisValue);
        }
    }
}
