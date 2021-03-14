using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ChangeUp : MonoBehaviour, IPointerUpHandler
{
    public List<GameObject> listSparrow;
    private  List<string> nameSparrow = new List<string>(); 
    private GameObject smallInstance;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < listSparrow.Count; i++)
        {
            nameSparrow.Add(listSparrow[i].name+"(Clone)(Clone)");
            
        }
        
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        for (int i = 0; i < nameSparrow.Count; i++)
        {
            smallInstance = GameObject.Find(nameSparrow[i]);
            if (smallInstance != null)
            {
                Vector3 rotationSmallVector = new Vector3(79.0f, -15.0f, -52.0f);
                smallInstance.transform.rotation = Quaternion.Euler(rotationSmallVector);
                break;
            }
            
        }
    }
}
