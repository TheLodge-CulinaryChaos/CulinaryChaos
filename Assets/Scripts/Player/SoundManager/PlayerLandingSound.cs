using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip landingSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLandingSound()
    {
        if (landingSound != null)
        {
            audioSource.PlayOneShot(landingSound);
        }
    }
}