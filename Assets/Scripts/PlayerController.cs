using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 10f;

    CharacterController characterController;

	// Use this for initialization
	void Awake()
    {
        characterController = GetComponent<CharacterController>();
	}
	
	void FixedUpdate()
    {
        Vector2 movementInput = GetHorizontalMovement();
        Vector3 movementVector = transform.forward * movementInput.y
                               + transform.right * movementInput.x;

        movementVector += Physics.gravity;

        float isJumping = Input.GetAxis("Jump");
        if (isJumping > 0f)
            movementVector.y = jumpSpeed;

        characterController.Move(movementVector * Time.deltaTime);
	}

    Vector2 GetHorizontalMovement()
    {
        // Read player input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed;
        if (isRunning)
            speed = runSpeed;
        else
            speed = walkSpeed;

        Vector2 movement = new Vector2(horizontal, vertical);
        if (movement.sqrMagnitude > 1)
            movement.Normalize();
        return movement * speed;
    }
}
