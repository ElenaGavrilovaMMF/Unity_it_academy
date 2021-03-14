using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChangeMaterial : MonoBehaviour, IPointerUpHandler
{
    private GameObject instance;
    private GameObject smallInstance;
    private GameObject[] listSparrow;
   
    // Start is called before the first frame update
    void Start()
    {
        listSparrow = Resources.LoadAll<GameObject>("Prefabs");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < listSparrow.Length; i++)
        {
            instance = GameObject.Find(listSparrow[i].name +"(Clone)");
            smallInstance = GameObject.Find(listSparrow[i].name +"(Clone)(Clone)");
            if (instance != null)
            {
                break;
            }
        }

        if (gameObject.name == "Red")
        {
            changeMaterial(Color.red);
        }
        if (gameObject.name == "Blue")
        {
            changeMaterial(Color.blue);
        }
        if (gameObject.name == "Yellow")
        {
            changeMaterial(Color.yellow);
        }
        if (gameObject.name == "Green")
        {
            changeMaterial(Color.green);
        }
    }

    private void changeMaterial(Color color)
    {
        instance.gameObject.GetComponent<MeshRenderer>().material.color = color; 
        smallInstance.gameObject.GetComponent<MeshRenderer>().material.color = color; 
    }
}
