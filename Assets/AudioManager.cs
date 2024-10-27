using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip footstepSound;
    public AudioClip landingSound;
    public AudioClip pickupSound;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();  // Automatically assigns the AudioSource if it's on the same GameObject
        }


    }

    public void PlayFootstepSound()
    {
            if (audioSource != null && footstepSound != null)
            {
                audioSource.clip = footstepSound;
                if (!audioSource.isPlaying)  // Ensure it doesn't overlap
                {
                    audioSource.Play();  // Plays the footstep sound continuously (useful for looping walking sounds)
                }
            }
    }

    public void PlayLandingSound()
    {
        if (audioSource != null && landingSound != null)
        {
            audioSource.clip = landingSound;
            audioSource.Play();  // Plays the landing sound
        }
    }

    public void PlayPickupSound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);  // PlayOneShot allows this sound to play without interrupting others
        }
    }

    


}
