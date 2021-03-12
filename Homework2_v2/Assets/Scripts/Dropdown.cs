using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    public Text subTitle;
    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.color = Color.white;
        //subTitle.fontSize = 35;
        subTitle.text = gameObject.name.Substring(8);
    }
}
