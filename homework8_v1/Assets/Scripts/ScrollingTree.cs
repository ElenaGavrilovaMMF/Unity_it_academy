using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTree : MonoBehaviour
{
    public float speed = 0.03f;

    private Transform cameraTransform;
    private Transform[] layers;
    private int leftIndex;
    private float backgroundSize;
    
    void Awake()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        backgroundSize = layers[layers.Length-1].transform.position.x;
    }
    
    void Update()
    {
        ScrollRigthMovingScreen();
        if (cameraTransform.position.x > (layers[layers.Length - 1].transform.position.x))
        {
            ScrollRigth();
        }
        
    }
    private void ScrollRigthMovingScreen()
    {
        transform.position = transform.position+Vector3.left*speed;
    }
    
    private void ScrollRigth()
    {
        layers[leftIndex].position = layers[leftIndex].position + Vector3.right*backgroundSize;
        leftIndex++;
        if (leftIndex==layers.Length)
        {
            leftIndex = 0;
        }
    }
}
