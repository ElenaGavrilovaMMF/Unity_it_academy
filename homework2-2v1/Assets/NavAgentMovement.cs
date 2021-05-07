using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    public GameObject door;
    public GameObject transformPoolBottom;
    
    private Camera cam;
    private NavMeshAgent agent;
    private float doorPosY;
    private float speed;
    private float poolWidth = 0.7f;
    private float waterSpeedCoeff = 3f;
    private float distanceFromDoor = 1f;
 
    void Start()
    { 
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        doorPosY = door.transform.position.y;
        speed = agent.speed;
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        Water();
        StartCoroutine(OpenDoor());
    }
    
    private IEnumerator OpenDoor()
    {
        float dist = Vector3.Distance(transform.TransformPoint(gameObject.transform.position),
                transform.TransformPoint(door.transform.position));
        if (dist<distanceFromDoor)
        {
            yield return new WaitForSeconds(0.2f);
            door.transform.position = new Vector3(door.transform.position.x, doorPosY + 1f,
                    door.transform.position.z);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            door.transform.position = new Vector3(door.transform.position.x, doorPosY,
                    door.transform.position.z);
        }
    }

    void Water()
    {
        Vector3 tempVector = new Vector3(transformPoolBottom.transform.position.x,gameObject.transform.position.y, transformPoolBottom.transform.position.z);
        float dist = Vector3.Distance(transform.TransformPoint(gameObject.transform.position),
            transform.TransformPoint(tempVector));
        if(dist < poolWidth ){
            agent.speed = speed / waterSpeedCoeff;
        }
        else
        {
            agent.speed = speed;
        }
    }
}
