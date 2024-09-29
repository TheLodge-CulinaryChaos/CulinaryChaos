using UnityEngine;

public class CameraManager : MonoBehaviour
{

    InputManager inputManager;

    // object camera follow
    public Transform targetTransform;
    public Transform cameraPivot; // object camera use

    private Transform cameraTransform; // transform of actual camera object
    public LayerMask collisionLayer;
    private float defaultPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;

    public float cameraCollisionOffset = 0.2f; // how much camera jump off of object if it collides
    public float minimumCollisionOffset = 0.2f; // minimum distance camera can be from object
    public float cameraCollisionRadius = 2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2f;
    public float cameraPivotSpeed = 2f;
    public float camLookSmoothTime = 1f;

    public float lookAngle; // up and down
    public float pivotAngle; // left and right
    public float minPivotAngle = -35f;
    public float maxPivotAngle = 35f;

    public float mouseXInput;
    public float mouseYInput;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;

        cameraTransform = Camera.main.transform; // init camera transform
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowPlayer();
        RotateCamera();
        HandleCameraCollisions();
    }


    private void FollowPlayer()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(
            transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;


        lookAngle += inputManager.cameraInputX * cameraLookSpeed;
        pivotAngle -= inputManager.cameraInputY * cameraPivotSpeed;

        lookAngle = Mathf.Lerp(lookAngle, lookAngle + (inputManager.cameraInputX * cameraLookSpeed), camLookSmoothTime * Time.deltaTime);

        pivotAngle = Mathf.Lerp(pivotAngle, pivotAngle - (inputManager.cameraInputX * cameraPivotSpeed), camLookSmoothTime * Time.deltaTime);

        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        // create sphere around object to detect collisions
        // store any hit in hit variable
        if (Physics.SphereCast(
            cameraPivot.transform.position,
            cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayer))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = -(distance - cameraCollisionOffset);

        }

        if (Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition -= minimumCollisionOffset;
        }
        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
