using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_OLD : MonoBehaviour
{
    private Animator animator;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;
    private float acceleration = 2.0f;
    private float deceleration = 2.0f;
    private float maxWalkVelocity = 0.5f;
    private float maxRunVelocity = 2.0f;
    private int VelocityZHash;
    private int VelocityXHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    void ChangeVelocity(bool moveForward, bool moveLeft, bool moveRight, float currentMaxVelocity)
    {
        if (moveForward && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        if (moveLeft && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        if (moveRight && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // DECELERATION if NOT WALKING FORWARD
        if (!moveForward && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // DECELERATION LEFT MOVE
        if (!moveLeft && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // DECELERATION RIGHT MOVE
        if (!moveRight && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
    }
    
    void ResetVelocity(bool moveForward, bool moveLeft, bool moveRight) {
        // IMMEDIATELY SET VELOCITY Z = 0 to STOP the WALKING animation
        // IF velocity < 0 (not walking)
        if (!moveForward && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // NOT MOVE LEFT, NOT MOVE RIGHT
        // and THE VELOCITY IS NEARLY TO IDLE
        // SET to 0 IMMEDIATELY
        if (!moveLeft && !moveRight && velocityX != 0.0f
        && velocityX > -0.05f && velocityX < 0.05f)
        {
            velocityX = 0.0f;
        }
    }

    void LockMovements(bool moveForward, bool moveLeft, bool moveRight, bool sprintPressed, float currentMaxVelocity) {
        LockForwardMovement(moveForward, sprintPressed, currentMaxVelocity);
        LockLeftMovement(moveLeft, sprintPressed, currentMaxVelocity);
        LockRightMovement(moveRight, sprintPressed, currentMaxVelocity);
    }

    #region Lock Movements
    void LockForwardMovement(bool moveForward, bool sprintPressed, float currentMaxVelocity) {
        //Lock foward
        if (moveForward && sprintPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }

        // decelearation to the max walk velocity
        else if (moveForward && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // round to current velocity if within offset
        else if (moveForward && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }
    }

    void LockLeftMovement(bool moveLeft, bool sprintPressed, float currentMaxVelocity) {
        if (moveLeft && sprintPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }

        // decelearation to the max walk velocity
        else if (moveLeft && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }
        // round to current velocity if within offset
        else if (moveLeft && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }
    }

    void LockRightMovement(bool moveRight, bool sprintPressed, float currentMaxVelocity) {
        if (moveRight && sprintPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }

        // decelearation to the max walk velocity
        else if (moveRight && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }
        // round to current velocity if within offset
        else if (moveRight && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }

    #endregion

    void Update()
    {
        bool moveForward = Input.GetKey(KeyCode.UpArrow);
        bool moveLeft = Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.RightArrow);
        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);

        float currentMaxVelocity = sprintPressed ? maxRunVelocity : maxWalkVelocity;

        #region Basic Movement

        ChangeVelocity(moveForward, moveLeft, moveRight, currentMaxVelocity);

        ResetVelocity(moveForward, moveLeft, moveRight);

        LockMovements(moveForward, moveLeft, moveRight, sprintPressed, currentMaxVelocity);

        #endregion

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);

    }
}
