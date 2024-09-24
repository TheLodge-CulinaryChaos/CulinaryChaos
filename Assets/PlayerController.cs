using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private int VelocityZHash;
    private int VelocityXHash;

    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;

    private Vector3 PlayerMovementInput;


    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    private void MovePlayer() {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);
        playerBody.AddForce(MoveVector, ForceMode.VelocityChange);

        if (Input.GetKeyDown(KeyCode.Space)) {
            playerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }


    }

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MovePlayer();

        // animator.SetFloat(VelocityXHash, PlayerMovementInput.x);
        // animator.SetFloat(VelocityZHash, PlayerMovementInput.z);
    }
}
