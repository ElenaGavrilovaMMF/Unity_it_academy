using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowRigthSparrow : MonoBehaviour, IPointerUpHandler
{
    private GameObject instance;
    private GameObject smallInstance;
    private GameObject[] listSparrow;

    // Start is called before the first frame update
    void Start()
    {
        listSparrow = Resources.LoadAll<GameObject>("Prefabs");
        for (int i = 0; i < listSparrow.Length; i++)
        {
            instance = GameObject.Find(listSparrow[i].name + "(Clone)");
        }
    }

    void Update()
    {
        OnMouseDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < listSparrow.Length; i++)
        {
            instance = GameObject.Find(listSparrow[i].name + "(Clone)");
            smallInstance = GameObject.Find(listSparrow[i].name + "(Clone)(Clone)");
            if (instance != null)
            {
                Vector3 position = instance.transform.position;
                Quaternion rotation = instance.transform.rotation;
                Material material = instance.gameObject.GetComponent<MeshRenderer>().material;
                Vector3 positionSmall = smallInstance.transform.position;
                Quaternion rotationSmall = smallInstance.transform.rotation;
               // Material materialSmall = smallInstance.gameObject.GetComponent<MeshRenderer>().material;
                if (instance != null)
                {
                    Destroy(instance);
                    Destroy(smallInstance);
                }

                if (i != listSparrow.Length-1)
                {
                    createLargeSpallow(position, rotation, material, i + 1);
                    createSmallSpallow(positionSmall, rotationSmall, material, i + 1);
                }
                else
                {
                    createLargeSpallow(position, rotation, material, 0);
                    createSmallSpallow(positionSmall, rotationSmall, material, 0);
                }

                break;
            }
        }
    }
    void OnMouseDrag()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (Input.mousePosition.y < 100.0f)
            {
                if (Input.mousePosition.x > 100.0f && Input.mousePosition.x < Screen.width - 100.0f)
                {
                    for (int i = 0; i < listSparrow.Length; i++)
                    {
                        if (instance != null)
                        {
                            Vector3 newRotation = new Vector3(instance.transform.rotation.x,
                                -Input.mousePosition.x - 90.0f,
                                instance.transform.rotation.z);
                            instance.transform.rotation = Quaternion.Euler(newRotation);
                            break;
                        }
                    }
                }
            }
        }
    }
    
    private void createSmallSpallow(Vector3 position, Quaternion rotation, Material material, int index)
    {
        smallInstance = Instantiate(listSparrow[index], position, rotation);
        smallInstance.transform.localScale = smallInstance.transform.localScale / 2;
        smallInstance.transform.name = smallInstance.transform.name + "(Clone)";
        smallInstance.gameObject.GetComponent<MeshRenderer>().material = material;
    }
    private void createLargeSpallow(Vector3 position, Quaternion rotation, Material material, int index)
    {
        instance = Instantiate(listSparrow[index], position, rotation);
        instance.gameObject.GetComponent<MeshRenderer>().material = material;
    }


}
