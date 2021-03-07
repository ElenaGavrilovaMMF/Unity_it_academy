using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private float timer = 0.0f;
    private float nSeconds ;
    public GameObject floor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timer > nSeconds)
        {
            Vector3 direction = generateNewPosition();
            transform.position = direction;
            nSeconds = Random.Range(1.0f, 5.0f);
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }
    private Vector3 generateNewPosition() 
    {
        float positionX = Random.Range(floor.transform.position.x - 9, floor.transform.position.x + 4);
        float positionY = Random.Range(floor.transform.position.y + 3, floor.transform.position.y + 4);
        float positionZ = Random.Range(floor.transform.position.z - 7, floor.transform.position.z + 6);
        return new Vector3(positionX, positionY, positionZ);
    }
}
