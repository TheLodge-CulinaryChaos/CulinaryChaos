using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerLocomotion playerLocomotion;

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
        HandleMovementInput();
        HandleSpringInput();

        HandleJumpInput();

        HandleCuttingInput();
        HandlePickupInput();

        HandlePauseInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSpringInput()
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
        if (pause_input) { // swap to get button up later
        Debug.Log("Pause Input");
            if (Input.GetKeyUp(KeyCode.P)) {
                Debug.Log("hello");
                if (canvasGroup.interactable) {
                    canvasGroup.interactable = false;
                    canvasGroup.blocksRaycasts = false;
                    canvasGroup.alpha = 0f;
                    Time.timeScale = 1f;
                } else {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    canvasGroup.alpha = 1f;
                    Time.timeScale = 0f;
                }
                pause_input = false;
            }
        }
    }
}