using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeRotationSmallSparrow : MonoBehaviour, IPointerUpHandler
{
    
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
            smallInstance = GameObject.Find(listSparrow[i].name+"(Clone)(Clone)");
            if (smallInstance != null)
            {
                if (gameObject.name == "Up")
                {
                    changeRotationSmallSparrow( new Vector3(79.0f, -15.0f, -52.0f));
                }
                if (gameObject.name == "Down")
                {
                    changeRotationSmallSparrow( new Vector3(-117.0f, 7.0f, 13.0f));
                }
                if (gameObject.name == "Left")
                {
                    changeRotationSmallSparrow( new Vector3(-186.0f, -69.5f, 169.0f));
                }
                if (gameObject.name == "Face")
                {
                    changeRotationSmallSparrow( new Vector3(-171.0f, -158.5f, 174.0f));
                }
                
                break;
            }
        }
    }

    private void changeRotationSmallSparrow(Vector3 vector)
    {
        Vector3 rotationSmallVector = vector; 
        smallInstance.transform.rotation = Quaternion.Euler(rotationSmallVector);
    }
}
