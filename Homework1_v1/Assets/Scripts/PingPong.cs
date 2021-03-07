using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PingPong : MonoBehaviour
{
    public GameObject prefab;
    public GameObject instance;
    public GameObject floor;
    public float speed = 2.0f;
    public float distance = 2.0f;
    private float distanceReverse;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Vector3 direction;
    
  
    // Start is called before the first frame update
    void Start()
    {
        startPosition = instance.transform.position;
        endPosition = startPosition;
        distanceReverse = distance;
        direction = generateNewPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(startPosition, instance.transform.position) < distance)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
            endPosition = instance.transform.position;
        }
        else
        {
            distance = 0.0f;
            if (Vector3.Distance(endPosition, instance.transform.position) < distanceReverse)
            {
                transform.Translate(-direction.normalized * speed * Time.deltaTime);
            }
            else
            {
                distance = distanceReverse;
            }
        }
    }
    private Vector3 generateNewPosition() 
    {
        float positionX = Random.Range(floor.transform.position.x - 9, floor.transform.position.x + 4);
        float positionY = Random.Range(floor.transform.position.y + 3, floor.transform.position.y + 4);
        float positionZ = Random.Range(floor.transform.position.z - 7, floor.transform.position.z + 6);
        return new Vector3(positionX, positionY, positionZ);
    }
}
