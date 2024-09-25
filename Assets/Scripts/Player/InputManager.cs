using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls; 
    PlayerLocomotion playerLocomotion;

    AnimatorManager animatorManager;
    Animator animator;
    public Vector2 movementInput;

    public float moveAmount;

    public bool shift_input;
    public bool cutting_input;


    public float verticalInput;
    public float horizontalInput;

    private void Awake() {
        playerLocomotion = GetComponent<PlayerLocomotion>();
        animatorManager = GetComponent<AnimatorManager>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable() {
        if (playerControls == null) {
            playerControls = new PlayerControls(); 
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            // action on shift key
            playerControls.PlayerActions.Shift.performed += i => shift_input = true;
            playerControls.PlayerActions.Shift.canceled += i => shift_input = false;

            // action on c key
            playerControls.PlayerActions.Cutting.performed += i => cutting_input = true;
            playerControls.PlayerActions.Cutting.canceled += i => cutting_input = false;
        }


        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    public void HandleAllInputs() {
        HandleMovementInput();
        HandleSpringInput();
        HandleCuttingInput();
    }

    private void HandleMovementInput() {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSpringInput() {
        if (shift_input && moveAmount > 0.5f) {
            playerLocomotion.isSprinting = true;
        } else {
            playerLocomotion.isSprinting = false;
        }
    }

    // need more work && conditions
    private void HandleCuttingInput() {
        if (cutting_input) {
            animatorManager.PlayTargetAnimation("Cutting", true);
        }
    }
}
