using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 3f;

    private Rigidbody rigidBody;

    public string currentGravity = "down";

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            rigidBody.velocity += transform.right * Input.GetAxisRaw("Horizontal") * playerSpeed * Time.deltaTime;
        }
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            rigidBody.velocity += transform.forward * Input.GetAxisRaw("Vertical") * playerSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.Translate(Vector3.up * jumpHeight * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentGravity == "down")
            {
                Physics.gravity = new Vector3(0, 4.0F, 0);
                currentGravity = "up";
            }
            else
            {
                Physics.gravity = new Vector3(0, -4.0F, 0);
                currentGravity = "down";
            }
        }
    }
}
