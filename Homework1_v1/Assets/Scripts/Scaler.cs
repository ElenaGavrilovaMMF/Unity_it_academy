using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    private float hightTree = 1.0f;
    private float hightSprout = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time, hightTree)+hightSprout, 
            Mathf.PingPong(Time.time, hightTree)+hightSprout,
            Mathf.PingPong(Time.time, hightTree)+hightSprout
            );
    }
}
