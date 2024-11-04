using UnityEngine;
using System.Collections;

public class hazardCollision : MonoBehaviour
{
    private bool isPaused = false;
    private float pauseDuration = 5f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Fire") && !isPaused)
        {
            StartCoroutine(PausePlayer());
        }
    }

    private IEnumerator PausePlayer()
    {
        isPaused = true;

        animator.SetBool("isHazard", true);

        var movementScript = GetComponent<PlayerManager>(); 
        if (movementScript != null)
        {
            movementScript.enabled = false;

        }

        // Wait for the specified pause duration
        yield return new WaitForSeconds(pauseDuration);

        animator.SetBool("isHazard", false);
        
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }

        isPaused = false;
    }
}
