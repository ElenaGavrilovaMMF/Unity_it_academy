using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    public float speed = 1f;
    public bool horizontal = true;
    
    private int pingPongFlag = 1;
    private float leftEdgeZ = 8f;
    private float rigthEdgeZ = 12f;
    private float leftEdgeX = 0f;
    private float rigthEdgeX = 4f;
    
    void Update()
    {
        if (horizontal)
        {
            if (!(Math.Round(gameObject.transform.position.z)>leftEdgeZ && Math.Round(gameObject.transform.position.z)<rigthEdgeZ))
            {
                pingPongFlag = -pingPongFlag;
            }
            gameObject.transform.position = gameObject.transform.position + pingPongFlag*Vector3.forward * speed*Time.deltaTime;
            
        }
        else
        {
            if (!(Math.Round(gameObject.transform.position.x)>leftEdgeX && Math.Round(gameObject.transform.position.x)<rigthEdgeX))
            {
                pingPongFlag = -pingPongFlag;
            }
            gameObject.transform.position = gameObject.transform.position + pingPongFlag*Vector3.right * speed*Time.deltaTime;
            
        }
    }
}
