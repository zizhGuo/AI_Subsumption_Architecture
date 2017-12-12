using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{

    // Use this for initialization
    void OnEnable()
    {
        Invoke("Destory", 2f);
    }

    private void Destory()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke(); 
    }

    // Update is called once per frame
    void Update()
    {

    }
}
