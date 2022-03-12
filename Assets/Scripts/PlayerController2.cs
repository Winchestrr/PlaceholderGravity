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
    public float speedChangeRate = 10f;
    public float gravityValue = -9.81f;
    public float playerSpeed;
    public float currentPlayerSpeed;
    public float sprintSpeed;
    public float bodyRotationSpeed;
    public float handRotationSpeed;

    public float targetRotation = 0f;
    public float rotationSmoothTime;
    public float verticalVelocity;
    private Vector3 targetDirection;

    [Header("Jump")]
    public bool isGrounded;
    public float groundedOffset;
    public float groundedRadius;
    public LayerMask groundLayers;
    public float jumpTimeout;
    public float fallTimeout;
    public float jumpHeight;

    private float _fallTimeoutDelta;
    private float _jumpTimeoutDelta;
    private float _fallVelocity;
    private float _verticalVelocity;


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
    }

    void Update()
    {
        GetInputs();
        NewMove();
        HandRotation();
        Jump();
    }

    void GetInputs()
    {
        input = moveAction.ReadValue<Vector2>();
        if (jumpAction.triggered) Cursor.visible = true;
        if (shootAction.triggered) gravityGun.Shoot("change");
        if (pickDropAction.triggered) gravityGun.Shoot("pick");
        if (yeetAction.triggered) gravityGun.Shoot("yeet");
    }

    void CheckGrounded()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }

    void Jump()
    {
        if (isGrounded)
        {
            _fallTimeoutDelta = fallTimeout;

            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;

            if (jumpAction.triggered && _jumpTimeoutDelta <= 0f)
                _verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * GameSystem.newGravity.y);

            if (_jumpTimeoutDelta >= 0f)
                _jumpTimeoutDelta -= Time.deltaTime;
        }
        else
        {
            _jumpTimeoutDelta = jumpTimeout;

            if (_fallTimeoutDelta >= 0f)
                _fallTimeoutDelta -= Time.deltaTime;
        }
    }

    void NewMove()
    {
        float targetSpeed = sprintAction.IsPressed() ? sprintSpeed : playerSpeed;

        if (moveAction.ReadValue<Vector2>() == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(controller.velocity.x, 0.0f, controller.velocity.z).magnitude;
        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
           currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            currentPlayerSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * speedChangeRate);
            currentPlayerSpeed = Mathf.Round(currentPlayerSpeed * 1000f) / 1000f;
        }
        else currentPlayerSpeed = targetSpeed;

        Vector3 inputDirection = new Vector3(moveAction.ReadValue<Vector2>().x,
                                             0f,
                                             moveAction.ReadValue<Vector2>().y);

        if(SwitchVCam.isAimed)
        {
            //make change here to not turn around when aiming

            if (moveAction.ReadValue<Vector2>() != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref bodyRotationSpeed, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
        }
        else
        {
            if (moveAction.ReadValue<Vector2>() != Vector2.zero)
            {
                targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref bodyRotationSpeed, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            }

            targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * Vector3.forward;
        }

        

        controller.Move(targetDirection.normalized * (currentPlayerSpeed * Time.deltaTime) +
                        new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);
    }

    //rotate while aimed
    //Quaternion bodyTargetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
    //transform.rotation = Quaternion.Lerp(transform.rotation, bodyTargetRotation, bodyRotationSpeed * Time.deltaTime);

    void HandRotation()
    {
        Quaternion handTargetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        hand.transform.rotation = Quaternion.Lerp(hand.transform.rotation, handTargetRotation, handRotationSpeed * Time.deltaTime);
    }
}
