using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Moving : MonoBehaviour
{
    public List <GameObject> gameObjectList = new List<GameObject> ();
    public GameObject floor;
    private GameObject currentObject;
    public float speed = 0.01f;
    void Start()
    {
        getLogListObjectsToConsole();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        { 
            var objectsCount = gameObjectList.Count;
            if (objectsCount != 0)
            {
                if (currentObject != null)
                {
                    Debug.Log(currentObject.name + " was deleted.");
                    Destroy(currentObject);
                    int objectID = Random.Range(0, objectsCount);
                    currentObject = createNextPrefab(objectID);
                    Debug.Log(currentObject.name + " was created.");
                }
                else
                {
                    int objectID = Random.Range(0, objectsCount);
                    currentObject = createNextPrefab(objectID);
                    Debug.Log(currentObject.name + " was created.");
                }
            }
            else
            {
                Debug.LogError("No objects!!!");
                return;
            }
        }
        
    }
    private GameObject createNextPrefab(int objectID)
    {
        GameObject selectedObject = gameObjectList[objectID];
        Vector3 startPosition = generateNewPosition();
        Quaternion newRoration = generateNewRotation();
        return Instantiate(selectedObject, startPosition, newRoration);
    }
    private Vector3 generateNewPosition() 
    {
        float positionX = Random.Range(floor.transform.position.x - 9, floor.transform.position.x + 4);
        float positionY = Random.Range(floor.transform.position.y + 3, floor.transform.position.y + 4);
        float positionZ = Random.Range(floor.transform.position.z - 7, floor.transform.position.z + 6);
        return new Vector3(positionX, positionY, positionZ);
        
    }
    
    private Quaternion generateNewRotation()
    {
        return Quaternion.identity;
    }

    private void getLogListObjectsToConsole()
    {
        string listOfNames = "";
        for (int i = 0; i < gameObjectList.Count; i++)
        {
            string nameObject = gameObjectList[i].name;
            listOfNames += ", " + nameObject;
        }
        Debug.Log(listOfNames);
    }

}