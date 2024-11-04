using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hazard : MonoBehaviour
{
    public List<GameObject> hazards; // List of hazards to control
    public float respawnTime = 5f; // Time in seconds before the hazards reappear

    private bool isAppear = true;

    private void Start()
    {
        StartCoroutine(ToggleHazards());
    }

    private IEnumerator ToggleHazards()
    {
        while (true)
        {
            isAppear = !isAppear; // Toggle the visibility state

            // Set each hazard's active state based on isAppear
            foreach (GameObject hazard in hazards)
            {
                if (hazard != null)
                {
                    hazard.SetActive(isAppear);
                }
            }

            yield return new WaitForSeconds(respawnTime); // Wait for the respawn time
        }
    }
}
