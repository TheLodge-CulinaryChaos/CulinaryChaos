using UnityEngine;
using System.Collections;

public class HazardCollision : MonoBehaviour
{
    private bool isPaused = false;
    private float pauseDuration = 5f;

    AnimatorManager animatorManager;
    GameObject currentHazard;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            animatorManager.animator.SetBool("isOnHazard", true);

            animatorManager.PlayTargetAnimation("OnFire", true);
            currentHazard = collision.gameObject;


            // set capsule collider's Y in the player
            transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 1f, 0);
        }
    }

    private void Update()
    {
        if (!animatorManager) return;

        if (currentHazard != null)
        {

            // turn off hazard if the currentHazard box collider is disabled
            if (!currentHazard.GetComponent<BoxCollider>().enabled)
            {
                animatorManager.animator.SetBool("isOnHazard", false);
                currentHazard = null;
                transform.GetComponent<CapsuleCollider>().center = new Vector3(0, 0.75f, 0);
            }
        }
    }
}
