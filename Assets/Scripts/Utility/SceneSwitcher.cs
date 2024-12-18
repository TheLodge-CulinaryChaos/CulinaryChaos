using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitcher : MonoBehaviour
{
    private BackgroundMusic backgroundMusic;
    public GameObject buttonGroup;
    public GameObject playerPicker;

    void Awake() {
        if (playerPicker) {
            playerPicker.SetActive(false);
        }
        MouseScript.ShowMouse();
    }

    // Start is called before the first frame update
    void Start() {
        backgroundMusic = FindObjectOfType<BackgroundMusic>(); 
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SelectPlayerMenu() {
        buttonGroup.SetActive(false);
        playerPicker.SetActive(true);
    }

    public void GeneralMenu() {
        buttonGroup.SetActive(true);
        playerPicker.SetActive(false);
    }

    public void switchScenes(string sceneName) {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 0f;
    }

    public void quitGame() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
