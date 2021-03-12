using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonOne : MonoBehaviour, IPointerDownHandler
{
    public Text subTitle;
    // Start is called before the first frame update
   
    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.text = gameObject.name + " Clicked"; 
    }
}
