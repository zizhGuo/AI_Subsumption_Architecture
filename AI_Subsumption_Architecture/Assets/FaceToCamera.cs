using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    Canvas canvas;
    public Camera mainCamera;
    // Use this for initialization
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //canvas.transform.LookAt(mainCamera.transform);
        canvas.transform.rotation = new Quaternion(canvas.transform.rotation.x, 0f, canvas.transform.rotation.z, canvas.transform.rotation.w);
    }
}
