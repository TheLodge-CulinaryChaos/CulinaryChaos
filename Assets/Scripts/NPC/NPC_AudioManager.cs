using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip frustratedSound;
    public AudioClip sittingSound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void PlayFrustratedSound()
    {
        if (audioSource != null && frustratedSound != null)
        {
            audioSource.clip = frustratedSound;
            if (!audioSource.isPlaying)  // Ensure it doesn't overlap
            {
                audioSource.Play();  // Plays the footstep sound continuously (useful for looping walking sounds)
            }
        }
    }

    public void PlaySittingSound()
    {
        if (audioSource != null && sittingSound != null)
        {
            audioSource.clip = sittingSound;
            audioSource.Play();
        }
    }


}
