using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    private Rigidbody body;
    private Camera _camera;
    private  GameObject weapon;
    private Vector3 throwSpeed = new Vector3(0.0f, 7.0f, 0.0f);
    void Start()
    {
        _camera = Camera.main;
        body = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        movingRobot();
       if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                Rigidbody rb = hitObject.GetComponent<Rigidbody>();
                StartCoroutine(SphereIndicator(hit.point));
            }
        }
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
    

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        weapon = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        weapon.gameObject.AddComponent<Rigidbody>();
        weapon.transform.position = gameObject.transform.position;
        weapon.gameObject.transform.rotation = Camera.main.transform.rotation;
        weapon.gameObject.GetComponent<Rigidbody>().AddForce(weapon.transform.forward * 10f + throwSpeed,
            ForceMode.Impulse);
        yield return new WaitForSeconds(1);
        
    }
    
    void movingRobot()
    {
        float sideForce = Input.GetAxis("Horizontal") * rotationSpeed;
        if (sideForce != 0.0f)
        {
            body.angularVelocity = new Vector3(0.0f, sideForce, 0.0f);
        }

        float forwardForce = Input.GetAxis("Vertical") * movementSpeed;
        if (forwardForce != 0.0f)
        {
            body.velocity = body.transform.forward * forwardForce;
        }
    }
}
