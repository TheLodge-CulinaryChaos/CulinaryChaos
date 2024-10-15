using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : MonoBehaviour
{
    private AudioSource audioSource;

    public AudioClip pickup;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickUpSound()
    {
        if (pickup != null)
        {
            audioSource.PlayOneShot(pickup);
        }
    }
}
