using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;
    PickUpController pickUpController;
    HoldingObjectScript holdingObjectScript;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffset = 0.5f;
    public LayerMask groundLayer;
    public LayerMask cookingLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    private float walkingSpeed = 1.5f;
    private float runningSpeed = 2.5f;
    private float sprintingSpeed = 4f;
    private float rotationSpeed = 7f;

    [Header("Jumping Speeds")]
    public float jumpHeight = 10;
    public float gravityIntensity = 9.8f;

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

        // get pickupcontroller
        pickUpController = GetComponent<PickUpController>();

        // get holdingobjectscript
        holdingObjectScript = pickUpController.HoldingObject.GetComponent<HoldingObjectScript>();

        // isGrounded = true;

    }

    public void HandleAllMovements()
    {
        // if player is on hazard, return and do not move
        if (playerManager.isOnHazard) return;

        HandleFallingAndLanding();

        // if player is interacting, return and do not move
        if (playerManager.isInteracting) return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isJumping)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection *= sprintingSpeed;
        }
        else if (!isGrounded)
        {
            moveDirection *= runningSpeed * 1.5f;
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
        if (isJumping)
        {
            return;
        }
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

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;

        rayCastOrigin.y += rayCastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);

                playerRigidbody.AddForce(transform.forward * leapingVelocity, ForceMode.Impulse);
            }
            inAirTimer += Time.deltaTime;

            playerRigidbody.AddForce(-Vector3.up * inAirTimer * fallingVelocity);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f, groundLayer)
            || Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, 0.5f, cookingLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleCutting()
    {
        animatorManager.PlayTargetAnimation("Cutting", true);
    }

    public void HandleJumping()
    {
        if (holdingObjectScript.IsHoldingPlate()) {
            // Debug.Log("Cannot jump while holding an object");
            return;
        }
        if (isGrounded)
        {
            // Debug.Log("Jumping");
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }

    public void HandlePickup()
    {
        pickUpController.HandleAllStates();
    }
}
