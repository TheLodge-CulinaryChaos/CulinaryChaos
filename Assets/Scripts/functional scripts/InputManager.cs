using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;
    PlayerManager playerManager;

    AnimatorManager animatorManager;
    public CanvasGroup canvasGroup;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public bool pause_input;

    [Header("Keyboard Input Flags")]
    public bool shift_input;
    public bool cutting_input;
    public bool jump_input;
    public bool pickup_input;
    public float verticalInput;
    public float horizontalInput;

    private void Awake()
    {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();

    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            // pause on p key
            playerControls.PlayerMovement.Pause.performed += i => pause_input = true;

            // action on shift key
            playerControls.PlayerActions.Shift.performed += i => shift_input = true;
            playerControls.PlayerActions.Shift.canceled += i => shift_input = false;

            // action on c key
            playerControls.PlayerActions.Cutting.performed += i => cutting_input = true;

            // action on space key
            playerControls.PlayerActions.Jump.performed += i => jump_input = true;

            // action on Q key
            playerControls.PlayerActions.Pickup.performed += i => pickup_input = true;
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {

        HandlePauseInput();

        if (Time.timeScale <= 0)
        {
            return;
        }

        HandleMovementInput();
        HandleSprintInput();

        HandleJumpInput();
        HandleCuttingInput();



        HandleDetection();

        HandlePickupInput();
    }

    private void HandleMovementInput()
    {
        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        if (playerManager.isOnHazard)
        {
            return;
        }
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleDetection()
    {
        playerLocomotion.HandleDetection();
    }

    private void HandleSprintInput()
    {
        if (shift_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }

    // need more work && conditions
    private void HandleCuttingInput()
    {
        if (cutting_input)
        {
            cutting_input = false;
            playerLocomotion.HandleCutting();
        }
    }

    private void HandlePickupInput()
    {
        if (pickup_input)
        {
            pickup_input = false;
            playerLocomotion.HandlePickup();
        }
    }


    private void HandleJumpInput()
    {
        if (jump_input)
        {
            jump_input = false;
            playerLocomotion.HandleJumping();
        }
    }
    private void HandlePauseInput()
    {
        if (pause_input)
        {
            if (canvasGroup.interactable)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.alpha = 0f;
                Time.timeScale = 1f;
                MouseScript.HideMouse();
            }
            else
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.alpha = 1f;
                Time.timeScale = 0f;
                MouseScript.ShowMouse();
            }
            pause_input = false;
        }
    }
}