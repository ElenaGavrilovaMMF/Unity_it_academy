using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Character : MonoBehaviour
{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 5f;
    public float rotationSpeed = 0.2f;
    public float animationBlendSpeed = 0.2f;
    public float jumpSpeed = 7f;
    
    private float rotationAngle = 0.0f;
    private float targetAnimationSpeed = 0f;
    private bool isSprint = false;
    private float speedY = 0f;
    private float gravity = -9.81f;
    private bool isJumping = false;
    private float power = 0f;
    private bool isDeath = false;
    
    private CharacterController controller;
    private Animator animator;
    private Camera characterCamera;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); }}
    public Camera CharacterCamera { get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }}
    public Animator CharacterAnimator { get { return animator = animator ?? GetComponent<Animator>(); }}
    
    void Start()
    {
        isJumping = true;
        CharacterAnimator.SetTrigger("Jump");
        speedY += jumpSpeed;
    }

    void Update()
    {
        if (!isDeath)
        {
            float vertical = Input.GetAxis("Vertical");
            float hotizontal = Input.GetAxis("Horizontal");

            Jumping();
            Fighting();
            StartCoroutine(Death());
            SetGravity();
            Landing();
            Moving(hotizontal, vertical);

        }
    }
    
    private IEnumerator Death()
    {
        if (Input.GetMouseButtonUp(1))
        {
            isDeath = true;
            CharacterAnimator.SetTrigger("Death");
            yield return new WaitForSeconds(2);
            CharacterAnimator.SetTrigger("Land");
            isDeath = false;
        }
    }

    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            speedY += jumpSpeed;
        }
    }

    void Fighting()
    {
        if (Input.GetMouseButtonUp(0))
        {
            power = Random.Range(0f, 4f);
            CharacterAnimator.SetFloat("Power", power);
            CharacterAnimator.SetTrigger("Fight");
        }
    }

    void SetGravity()
    {
        if (!Controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if (speedY < 0f)
        {
            speedY = 0f;
        }
        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);
    }

    void Landing()
    {
        if (isJumping && speedY < 0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }
    }

    void Moving(float hotizontal, float vertical)
    {
        isSprint = Input.GetKey(KeyCode.LeftShift);
        Vector3 movement = new Vector3(hotizontal, 0f, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) *
                                  movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;
        float currentSpeed = isSprint ? sprintSpeed : movementSpeed;
        Controller.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);

        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            targetAnimationSpeed = isSprint ? 1f : 0.5f;
        }
        else
        {
            targetAnimationSpeed = 0f;
        }

        CharacterAnimator.SetFloat("Speed",
            Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
        Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
    }
}