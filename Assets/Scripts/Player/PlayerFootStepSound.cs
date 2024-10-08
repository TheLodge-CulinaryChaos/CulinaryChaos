using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip footstepSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound()
    {
        // Play the footstep sound when called
        if (footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
