using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public float jumpSpeed = 8f;
    public float minViewingAngleY = -20f;
    public float maxViewingAngleY = 45f;
    
    private float rotationY = 0f;
    private Quaternion originalCameraRotation;
    private Vector3 movement;
    private CharacterController controller;
    private Camera camera;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public Camera MainCamera { get { return camera = camera ?? FindObjectOfType<Camera>(); } }
    
    void Start()
    {
        movement = new Vector3(0f, gravity, 0f);
        originalCameraRotation = MainCamera.transform.localRotation;
    }

    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            CharacterMove();
            MouseMove();
        }
        else
        {
            CharacterMoveAndroid();
        }
    }

    void CharacterMoveAndroid()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (movement.y > gravity) movement.y += gravity * Time.deltaTime;
            movement.z = Input.acceleration.x;
            if (movement.sqrMagnitude > 1) movement.Normalize();
            movement *= Time.deltaTime;
            Controller.Move(transform.TransformDirection(movement) * speed * speed);
            
            Touch touchRigth = Input.GetTouch(1);
            if (touchRigth.phase == TouchPhase.Moved) MouseMove();
            if (touchRigth.phase == TouchPhase.Began) movement.y = jumpSpeed;
        }
    }

    void CharacterMove()
    {
        if (Controller.isGrounded)
        {
            float vertical = Input.GetAxis("Vertical");
            float hotizontal = Input.GetAxis("Horizontal");
            movement = new Vector3(hotizontal, gravity, vertical);
            movement *= speed;
            if (Input.GetKeyDown(KeyCode.Space)) movement.y = jumpSpeed;
        }
        if (movement.y > gravity) movement.y += gravity * Time.deltaTime;
        Controller.Move(transform.TransformDirection(movement)  * Time.deltaTime);
    }

    void MouseMove()
    {
        float rotationX = Input.GetAxis("Mouse X");
        rotationY += Input.GetAxis("Mouse Y");
        rotationY = ClampAngle(rotationY, minViewingAngleY, maxViewingAngleY);
        Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
        MainCamera.transform.localRotation = originalCameraRotation * yQuaternion;
        Controller.transform.Rotate(Vector3.up, rotationX);
    }
    
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle>360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }

    void AndroidMousMove()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;
        if (dir.sqrMagnitude > 1)
            dir.Normalize();
        dir *= Time.deltaTime;
        transform.Translate(transform.TransformDirection(dir) * speed);
    }
}
