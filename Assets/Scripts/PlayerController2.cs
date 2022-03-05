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
    public float bodyRotationSpeed;
    public float handRotationSpeed;

    private Vector2 input;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction pickDropAction;

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
        shootAction = playerInput.actions["Shoot"];
        pickDropAction = playerInput.actions["PickUpDrop"];
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
}
