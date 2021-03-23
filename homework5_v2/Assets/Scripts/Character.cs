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
    public float minViewingAngleY = -20f;
    public float maxViewingAngleY = 45f;
    public Camera camera;
    public float rotSpeed = 1.5f;
    public float jumpSpeed = 15f;
    public float terminalVelocity = -10f;
    public float minFall = -1.5f;
    
    private CharacterController controller;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    private Vector3 distCharacterCamera;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private float rotationY = 0f;
    private float rotationX = 0f;
 

   void Start()
    {
        vertSpeed = minFall;
        rotationX = camera.transform.eulerAngles.y;
        distCharacterCamera = transform.position - camera.transform.position;
    }

    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement = CreateCharacterMovementVector(horizontal, vertical);
            //это что-бы убрать эффект "горной козы". 
            bool hitGround = CheckHitGround();
            if (hitGround)
            {
                SetJumpSpeed();
            }
            else
            {
                SetVerticalSpeed();
                CheckControllerWithHigthCollider(movement);
            }
            movement.y = vertSpeed;
            movement *= Time.deltaTime;
            Controller.Move(movement);
        }
        else
        {
            Vector3 movementAndroid = transform.TransformDirection(new Vector3(-1f, gravity, 1f));
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    MoveCharacterAndroid(movementAndroid);
                    MoveCamera();
                }
            }
        }
    }

    void LateUpdate()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal != 0) rotationX += horizontal * rotSpeed;
                else rotationX += Input.GetAxis("Mouse X") * rotSpeed * 3;
            
            MoveCamera();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
    }

    bool CheckHitGround()
    {
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = (Controller.height + Controller.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        return hitGround;
    }
    void MoveCamera()
    {
        rotationY += Input.GetAxis("Mouse Y") * rotSpeed * 3;
        rotationY = ClampAngle(rotationY, minViewingAngleY, maxViewingAngleY);
        Quaternion rotation = Quaternion.Euler(-rotationY, rotationX, 0);
        camera.transform.position = transform.position - (rotation * distCharacterCamera);
        camera.transform.LookAt(gameObject.transform);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f) angle += 360f;
        if (angle>360f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
    
    void SetJumpSpeed()
    {
        if (Input.GetButtonDown("Jump"))
            vertSpeed = jumpSpeed;
    }
    
    void SetVerticalSpeed()
    {
        vertSpeed += gravity * 5 * Time.deltaTime;
        if (vertSpeed < terminalVelocity) vertSpeed = terminalVelocity;
    }

    void CheckControllerWithHigthCollider(Vector3 movement)
    {
        if (Controller.isGrounded)
        {
            if (Vector3.Dot(movement, contact.normal) < 0) movement = contact.normal * speed;
            else movement += contact.normal * speed;
        }
    }

    private Vector3 CreateCharacterMovementVector(float horizontal, float vertical)
    {
        Vector3 movement = Vector3.zero;
        if (horizontal != 0 || vertical != 0)
        {
            movement.x = horizontal * speed;
            movement.z = vertical * speed;
            movement = Vector3.ClampMagnitude(movement, speed);
            Quaternion tmp = camera.transform.rotation;
            camera.transform.eulerAngles = new Vector3(0, camera.transform.eulerAngles.y, 0);
            movement = camera.transform.TransformDirection(movement);
            camera.transform.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * 10f * Time.deltaTime);
        }

        return movement;
    }

    void MoveCharacterAndroid(Vector3 movementAndroid)
    {
        movementAndroid *= speed;
        rotationX += Input.GetAxis("Mouse X") * rotSpeed * 3;
        Quaternion rotation = Quaternion.Euler(0, rotationX, 0);
        transform.rotation = rotation;
        Controller.Move(movementAndroid * Time.deltaTime);
    }

}
