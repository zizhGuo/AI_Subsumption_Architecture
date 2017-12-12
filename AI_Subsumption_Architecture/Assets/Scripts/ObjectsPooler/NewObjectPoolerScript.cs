using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewObjectPoolerScript : MonoBehaviour
{
    public GameObject pooledObject;
    public int pooledAmount = 5;
    public bool willGrow;
    private int activePooledObject = 0;
    public List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        if (willGrow)
        {
            GameObject obj1 = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(obj1);
            return obj1;
        }
        return null;

    }
}
