using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController2 : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private PlayerInput playerInput;
    public Transform cameraTransform;
    public GravityGun gravityGun;
    public GameObject hand;

    public Vector3 playerVelocity;
    public float jumpHeight;
    public float gravityValue = -9.81f;
    public float playerSpeed;
    public float sprintSpeed;
    public float bodyRotationSpeed;
    public float handRotationSpeed;

    [Header("Jump")]
    public bool isGrounded;
    public float groundedOffset;
    public float groundedRadius;
    public LayerMask groundLayers;

    private Vector2 input;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction shootAction;
    private InputAction pickDropAction;
    private InputAction yeetAction;

    //https://www.youtube.com/watch?v=SeBEvM2zMpY 39:10

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        //cameraTransform = Camera.main.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];
        shootAction = playerInput.actions["Shoot"];
        pickDropAction = playerInput.actions["PickUpDrop"];
        yeetAction = playerInput.actions["Yeet"];
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        //shootAction.performed += _ => gravityGun.ChangeGravity();
    }

    private void OnDisable()
    {
        //shootAction.performed -= _ => gravityGun.ChangeGravity();
    }



    void Update()
    {
        GetInputs();
        Move();
    }


    void GetInputs()
    {
        input = moveAction.ReadValue<Vector2>();
        if (jumpAction.triggered) Cursor.visible = true;
        if (shootAction.triggered) gravityGun.Shoot("change");
        if (pickDropAction.triggered) gravityGun.Shoot("pick");
        if (yeetAction.triggered) gravityGun.Shoot("yeet");
    }

    void Move()
    {
        if(playerVelocity.y < 0) playerVelocity.y = 0;

        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        direction = direction.x * cameraTransform.right.normalized +
                    direction.z * cameraTransform.forward.normalized;
        direction.y = 0f;

        controller.Move(direction * Time.deltaTime * playerSpeed);

        if(jumpAction.triggered)
        {
            Debug.Log("jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotate
        Quaternion bodyTargetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, bodyRotationSpeed * Time.deltaTime);

        Quaternion handTargetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation, handTargetRotation, handRotationSpeed * Time.deltaTime);

    }

    void CheckGrounded()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    void NewMove()
    {
        float targetSpeed = sprintAction.IsPressed() ? sprintSpeed : playerSpeed;

        //if (moveAction. == Vector2.zero) targetSpeed = 0.0f;
    }
}
