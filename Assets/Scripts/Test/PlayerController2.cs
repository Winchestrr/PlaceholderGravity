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

    public Vector3 playerVelocity;
    public float jumpHeight;
    public float gravityValue = -9.81f;
    public float playerSpeed;

    private Vector2 input;
    private InputAction moveAction;
    private InputAction jumpAction;

    //https://www.youtube.com/watch?v=SeBEvM2zMpY

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void Update()
    {
        GetInputs();
        Move();
    }


    void GetInputs()
    {
        input = moveAction.ReadValue<Vector2>();
    }

    void Move()
    {
        if(playerVelocity.y < 0) playerVelocity.y = 0;

        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        controller.Move(direction * Time.deltaTime * playerSpeed);

        if(jumpAction.triggered)
        {
            Debug.Log("jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
