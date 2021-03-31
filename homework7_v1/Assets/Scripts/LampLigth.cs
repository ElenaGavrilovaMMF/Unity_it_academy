using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampLigth : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(LigthChange());
    }
    
    private IEnumerator LigthChange()
    {
        gameObject.GetComponent<Light>().intensity = 10;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Light>().intensity = 1;
        
    }
}
