using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WrappinrText : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI text;
    void Start()
    {
        text.enableWordWrapping = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
