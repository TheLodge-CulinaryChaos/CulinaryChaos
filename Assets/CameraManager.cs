using UnityEngine;

public class CameraManager : MonoBehaviour
{

    // object camera follow
    public Transform targetTransform;
    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.2f;

    private void Awake() {
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }

    public void FollowPlayer()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(
            transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        
        transform.position = targetPosition;
    }
}
