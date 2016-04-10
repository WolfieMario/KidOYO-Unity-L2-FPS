using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : DeathController
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpSpeed = 10f;

    public Vector2 mouseSensitivity = new Vector2(2, 2);
    public float minimumXRotation = -90;
    public float maximumXRotation = 90;

    CharacterController characterController;
    Camera camera;

	// Use this for initialization
	void Awake()
    {
        characterController = GetComponent<CharacterController>();
        camera = Camera.main;
	}

    void Update()
    {
        MouseLook();
        UpdateCursorLock();
    }
	
	void FixedUpdate()
    {
        Vector2 movementInput = GetHorizontalMovement();
        Vector3 movementVector = transform.forward * movementInput.y
                               + transform.right * movementInput.x;

        if (!characterController.isGrounded)
            movementVector.y = characterController.velocity.y;
        movementVector += Physics.gravity * Time.deltaTime;

        float isJumping = Input.GetAxis("Jump");
        if (isJumping > 0f && characterController.isGrounded)
            movementVector.y = jumpSpeed;

        characterController.Move(movementVector * Time.deltaTime);
	}

    void MouseLook()
    {
        float rotationY = Input.GetAxis("Mouse X") * mouseSensitivity.x;
        float rotationX = Input.GetAxis("Mouse Y") * mouseSensitivity.y;

        characterController.transform.localRotation *= Quaternion.Euler(0f, rotationY, 0f);
        camera.transform.localRotation = ClampRotationAroundXAxis(camera.transform.localRotation * Quaternion.Euler(-rotationX, 0f, 0f));

//        Quaternion cameraRotation = camera.transform.localRotation * Quaternion.Euler(-rotationX, 0f, 0f);
//		Vector3 cameraEulerRotation = cameraRotation.eulerAngles;
//        cameraEulerRotation.x = Mathf.Clamp((cameraEulerRotation.x + 180) % 360 - 180,
//                                            minimumXRotation, maximumXRotation);
//        cameraEulerRotation.y = 0f;
//        cameraEulerRotation.z = 0f;
//        cameraRotation.eulerAngles = cameraEulerRotation;
//        camera.transform.localRotation = cameraRotation;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, minimumXRotation, maximumXRotation);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    void UpdateCursorLock()
    {
        if (Input.GetKey(KeyCode.Escape))
            UnlockMouse();
        else if (Input.GetMouseButton(0))
            LockMouse();
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    public override void OnDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
