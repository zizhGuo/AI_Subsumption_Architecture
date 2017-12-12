using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPlacer : MonoBehaviour
{
    public GameObject obstaclePooler;
    public GameObject chickenPooler;
    //public GameObject wolfiePooler;
    public GameObject wolf;

    private NewObjectPoolerScript rock;
    private NewObjectPoolerScript chicken;
    //private NewObjectPoolerScript wolfie;

    Camera mainCamera;
    Vector3 mousePos;
    bool _hit;
    RaycastHit hit;
    int layerMask;

    public int chosenNum;
    //public List<int> chosenNumLst; // +++++++++++++++++++++++

    void Start()
    {
        mainCamera = GetComponent<Camera>();
        rock = obstaclePooler.GetComponent<NewObjectPoolerScript>();
        chicken = chickenPooler.GetComponent<NewObjectPoolerScript>();
        //wolfie = wolfiePooler.GetComponent<NewObjectPoolerScript>();
        layerMask = 1 << 10;
    }
    void FixedUpdate()
    {
        mousePos = Input.mousePosition;
        Ray ray = mainCamera.ScreenPointToRay(mousePos);
        _hit = Physics.Raycast(ray, out hit, 100,layerMask);
        Debug.DrawLine(transform.position, hit.point, Color.red);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < rock.pooledAmount; i++)
            {
                if (!rock.pooledObjects[i].activeInHierarchy)
                {
                    rock.pooledObjects[i].transform.position = new Vector3(hit.point.x, 0f, hit.point.z); // hit.transform.position;
                    rock.pooledObjects[i].SetActive(true);
                    break;
                }
                if (rock.pooledObjects[i] == null) return;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < chicken.pooledAmount; i++)
            {
                if (!chicken.pooledObjects[i].activeInHierarchy)
                {
                    chicken.pooledObjects[i].transform.position = new Vector3(hit.point.x, 0f, hit.point.z); // hit.transform.position;
                    chicken.pooledObjects[i].SetActive(true);
                    break;
                }
                if (chicken.pooledObjects[i] == null) return;
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GameObject obj = (GameObject)Instantiate(wolf);
            obj.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
        }
    }
}
