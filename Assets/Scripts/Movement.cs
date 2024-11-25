using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 2f;
    public float runSpeed = 6f;
    public float jumpPower = 7f; // Normal jump power
    public float antiGravityJumpPower = 9f; // Slightly higher jump power during anti-gravity
    public float gravity = 7f; // Normal gravity
    public float reducedGravity = 7f; // Reduced gravity during anti-gravity (lets player stay in air longer)
    public float lookSpeed = 3f;
    public float lookXLimit = 45f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;
    private bool isAntiGravity = false; // Flag for reduced gravity phase

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Start the anti-gravity timer after an initial delay
        StartCoroutine(AntiGravityCycle());
    }

    void Update()
    {
        // Disable movement and camera controls if the game is paused
        if (Time.timeScale == 0f)
        {
            return;
        }

        // Movement logic
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            // Apply different jump power depending on anti-gravity state
            moveDirection.y = isAntiGravity ? antiGravityJumpPower : jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            // Apply reduced gravity or normal gravity based on the state
            moveDirection.y -= (isAntiGravity ? reducedGravity : gravity) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Camera rotation logic
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    IEnumerator AntiGravityCycle()
    {
        yield return new WaitForSeconds(10f); // Initial delay before anti-gravity starts

        while (true)
        {
            // Enable anti-gravity
            isAntiGravity = true;
            yield return new WaitForSeconds(5f); // Anti-gravity duration (player stays in air longer)

            // Disable anti-gravity
            isAntiGravity = false;
            yield return new WaitForSeconds(10f); // Normal gravity duration
        }
    }
}
