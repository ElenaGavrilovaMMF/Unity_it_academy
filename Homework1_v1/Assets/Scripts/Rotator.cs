using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Random.Range(-1.0f, 1.0f), 
            Random.Range(-1.0f, 1.0f), 
            Random.Range(-1.0f, 1.0f));
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(direction * speed * Time.deltaTime, Space.Self);
    }
}
