using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDisable : MonoBehaviour, IPointerDownHandler
{
    private Button[] buttonsArray;
    private List<Button> buttonsList = new List<Button>();
    // Start is called before the first frame update
    void Start()
    {
        buttonsArray = GameObject.FindObjectsOfType<Button>();
        convertArrayToListButton(buttonsList,"secondLevelButtons");
    }

    // Update is called once per frame
    public void OnPointerDown(PointerEventData eventData)
    {
        for (int i = 0; i < buttonsList.Count; i++)
              {
                  buttonsList[i].enabled = false;
              }
    }
    
    private void convertArrayToListButton(List<Button> listButtons, string tag)
    {
        for (int i = 0; i < buttonsArray.Length; i++)
        {
            if (buttonsArray[i].tag == tag)
            {
                listButtons.Add(buttonsArray[i]);
            }
        }
        listButtons.Reverse();
    }
}
