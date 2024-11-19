using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip track01;
    public AudioClip track02;
    public AudioClip track03;
    public AudioClip track04;
    public AudioClip track05;
    public AudioClip track06;
    private string currentScene;

    // create dictionary for ost
    Dictionary<string, AudioClip> songList = new Dictionary<string,AudioClip>();

    void Awake() {
        currentScene = SceneManager.GetActiveScene().name;

        songList.Add("TItle Screen", track01);
        songList.Add("Instructions", track02);
        songList.Add("Credits", track02); 
        songList.Add("Level Select", track02);    
        songList.Add("Player Select", track02);                 
        songList.Add("Easy", track03);
        songList.Add("Medium", track04);
        songList.Add("Hard", track05);

        // add triumphant song i guess
        songList.Add("End", track02);        
        
        audioSource = GetComponent<AudioSource>();
        playBGTrack(currentScene);
        Debug.Log("test");
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Plays the Track
    public void playBGTrack(string sceneName) {
        if (audioSource.isPlaying) {
            audioSource.Stop();
        }
        if (audioSource != null && songList[sceneName] != null) {
            audioSource.clip = songList[sceneName];
            audioSource.Play(); 
        }
    }
}
