using System.Collections;
using System.Collections.Generic;
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
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start() {
        songList.Add("TItle Screen", track01);
        songList.Add("Instructions", track02);       
        songList.Add("Easy", track03);
        songList.Add("Medium", track04);
        songList.Add("Hard", track05);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Plays the Track
    public void playBGTrack(string sceneName) {
        currentScene = sceneName;
        if (audioSource != null && songList[sceneName] != null) {
            audioSource.clip = songList[sceneName];
            if (!audioSource.isPlaying) {
                audioSource.Play(); 
            }
        }
    }
}
