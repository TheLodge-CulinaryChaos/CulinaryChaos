using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header ("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;

    [Header("Movement Speeds")]
    private float walkingSpeed = 1.5f;
    private float runningSpeed = 2.5f;
    private float sprintingSpeed = 4f;
    private float rotationSpeed = 7f;

    public void Awake()
    {
        // get playermanager same object
        playerManager = GetComponent<PlayerManager>();
        // get animatormanager same object
        animatorManager = GetComponent<AnimatorManager>();

        // get inputmanager same object
        inputManager = GetComponent<InputManager>();

        // get rigidbody
        playerRigidbody = GetComponent<Rigidbody>();

        // get camera object
        cameraObject = Camera.main.transform;

        // isGrounded = true;

    }

    public void HandleAllMovements()
    {

        HandleFallingAndLanding();

        // if player is interacting, return and do not move
        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;


        if (isSprinting)
        {
            moveDirection *= sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection *= runningSpeed;
            }
            else
            {
                moveDirection *= walkingSpeed;
            }
        }

        moveDirection *= runningSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding() {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        
        rayCastOrigin.y += rayCastHeightOffset;

        if (!isGrounded) {
            if (!playerManager.isInteracting) {
                Debug.Log("Falling");
                animatorManager.PlayTargetAnimation("Falling", true);

                playerRigidbody.AddForce(transform.forward * leapingVelocity, ForceMode.Impulse);
            }
            inAirTimer += Time.deltaTime;

            playerRigidbody.AddForce(-Vector3.up * inAirTimer * fallingVelocity);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting) {
                animatorManager.PlayTargetAnimation("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else {
            isGrounded = false;
        }
    }

}
