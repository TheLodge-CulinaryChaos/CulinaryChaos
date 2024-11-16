using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hazard : MonoBehaviour
{
    public List<GameObject> hazards; // List of hazards to control
    public float minRespawnTime = 7f; // Time in seconds before the hazards reappear
    public float maxRespawnTime = 10f; // Time in seconds before the hazards reappear

    private bool isFiring = false;

    private void Start()
    {
        StartCoroutine(ToggleHazards());
    }

    private void PlayTargetAnimation(Animator animator, string targetAnimation)
    {
        animator.CrossFade(targetAnimation, 0.2f);
    }

    private IEnumerator ToggleHazards()
    {
        while (true)
        {

            float randomTime = Random.Range(4f, 5f);
            float randomRespawnTime = Random.Range(minRespawnTime, maxRespawnTime);

            foreach (GameObject hazard in hazards)
            {
                Animator hazardAnimator = hazard.GetComponent<Animator>();
                if (hazard != null) {
                    // enable box collider
                    PlayTargetAnimation(hazardAnimator, "Start Fire");
                    isFiring = true;
                    hazard.GetComponent<BoxCollider>().enabled = isFiring;
                    hazard.GetComponent<BoxCollider>().isTrigger = isFiring;
                    hazardAnimator.SetBool("isFiring", isFiring);
                }
            }

            yield return new WaitForSeconds(randomTime); // Wait for at least 2 seconds

            foreach (GameObject hazard in hazards)
            {
                Animator hazardAnimator = hazard.GetComponent<Animator>();
                if (hazard != null)
                {
                    // disable box collider
                    isFiring = false;
                    hazard.GetComponent<BoxCollider>().enabled = isFiring;
                    hazard.GetComponent<BoxCollider>().isTrigger = isFiring;
                    hazardAnimator.SetBool("isFiring", isFiring);
                }
            }

            yield return new WaitForSeconds(randomRespawnTime - randomTime); // Wait for the remaining respawn time
        }
    }
}
