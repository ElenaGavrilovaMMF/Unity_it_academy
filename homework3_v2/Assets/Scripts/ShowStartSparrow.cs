using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
public class ShowStartSparrow : MonoBehaviour
{
    private GameObject[] listSparrows;
    private GameObject sparrow;

    // Start is called before the first frame update
    void Start()
    {
        listSparrows = Resources.LoadAll<GameObject>("Prefabs");
        createLargeSparrow();
        createLittleSparrow();
    }

    void Update()
    {
        OnMouseDrag();
    }

    private void createLargeSparrow()
    {
        Vector3 position = new Vector3(259.0f, -1.0f, 18.0f);
        Quaternion rotation = Quaternion.identity;
        sparrow = Instantiate(listSparrows[0], position, rotation);
        sparrow.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void createLittleSparrow()
    {
        Vector3 positionSmallSparrow=new Vector3(250.0f, 12.5f, 18.0f);
        Vector3 rotationSmallVector = new Vector3(79.0f, -15.0f,-52.0f);
        Quaternion ratationSmall = Quaternion.Euler(rotationSmallVector);
        GameObject smallSparrow = Instantiate(sparrow, positionSmallSparrow, ratationSmall);
        smallSparrow.transform.localScale = smallSparrow.transform.localScale / 3;
        smallSparrow.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }
    
    void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.mousePosition.y < 100.0f)
            {
                if (Input.mousePosition.x > 100.0f && Input.mousePosition.x < Screen.width - 100.0f)
                {
                    for (int i = 0; i < listSparrows.Length; i++)
                    {
                        if (sparrow != null)
                        { Vector3 newRotation = new Vector3(sparrow.transform.rotation.x,
                                -Input.mousePosition.x - 90.0f,
                                sparrow.transform.rotation.z);
                            sparrow.transform.rotation = Quaternion.Euler(newRotation);
                            break;
                        }
                       
                    }
                }
            }
        }
    }
}
