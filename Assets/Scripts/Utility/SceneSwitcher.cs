using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    private BackgroundMusic backgroundMusic;

    void Awake() {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
    }

    // Start is called before the first frame update
    void Start() {
        backgroundMusic = FindObjectOfType<BackgroundMusic>(); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void switchScenes(string sceneName) {
        SceneManager.LoadScene(sceneName);
        //backgroundMusic.playBGTrack(sceneName);
        Time.timeScale = 1f;
    }

    public void quitGame() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
