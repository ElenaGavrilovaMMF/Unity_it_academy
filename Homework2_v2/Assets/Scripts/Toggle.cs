using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Toggle : MonoBehaviour, IPointerDownHandler
{
    public Toggle togglesArray;
    public Text subTitle;
    // Start is called before the first frame update
    void Start()
    {
       // togglesArray = GameObject.FindObjectsOfType<Toggle>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        subTitle.text = gameObject.name;
    }

}
