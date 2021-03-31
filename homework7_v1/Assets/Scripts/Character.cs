using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Audio;


public class Character : MonoBehaviour
{
    public float movementSpeed = 20.0f;
    public float rotationSpeed = 0.01f;
    
    public GameObject previousFloor;
    public GameObject nextFloor;
    public GameObject currentFloor;
    public GameObject walls;
    public AudioMixerSnapshot floor1;
    public AudioMixerSnapshot floor2;
    public GameObject blood;
    
    private bool changeAudio;
    private float rotationAngle = 0.0f;
    private float gravity = -9.81f;


    private CharacterController controller;
    private Camera characterCamera;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); }}
    public Camera CharacterCamera { get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }}
    
    void Start()
    {
        changeAudio = false;
    }

    void Update()
    {
            float vertical = Input.GetAxis("Vertical");
            float hotizontal = Input.GetAxis("Horizontal");
            Moving(hotizontal, vertical);
    }
    
    void Moving(float hotizontal, float vertical)
    {
        Vector3 movement = new Vector3(hotizontal, gravity, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                                  movement.normalized;
        
        Controller.Move((rotatedMovement * movementSpeed) * Time.deltaTime);
        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
        }
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 floorDist = new Vector3(0f, -1.36f, 0f);
        createNewFloor(floorDist);
        changeFloorBackgroundAudio();
        createBloodSphere(floorDist);
        other.isTrigger = false;
    }

    private void createNewFloor(Vector3 floorDist)
    {
        
        Destroy(previousFloor);
        GameObject floor = Instantiate(nextFloor, nextFloor.transform.position + floorDist,
            nextFloor.transform.rotation);
        previousFloor = currentFloor;
        currentFloor = nextFloor;
        nextFloor = floor;
        walls.transform.position = walls.transform.position + floorDist;
    }

    private void changeFloorBackgroundAudio()
    {
        changeAudio = !changeAudio;
        if (changeAudio)
            floor2.TransitionTo(0.5f);
        else
            floor1.TransitionTo(0.5f);
    }

    private void createBloodSphere(Vector3 floorDist)
    {
        GameObject bloodTemp = Instantiate(blood, blood.GetComponent<BloodAudio>().startPos + floorDist, Quaternion.identity);
        Destroy(blood);
        blood = bloodTemp;
    }
}