using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]

public class PauseMenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    
    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        // if (Input.GetKeyUp (KeyCode.P)) { // swap to get button up later
        //     if (canvasGroup.interactable) {
        //         canvasGroup.interactable = false;
        //         canvasGroup.blocksRaycasts = false;
        //         canvasGroup.alpha = 0f;
        //         Time.timeScale = 1f;
        //     } else {
        //         canvasGroup.interactable = true;
        //         canvasGroup.blocksRaycasts = true;
        //         canvasGroup.alpha = 1f;
        //         Time.timeScale = 0f;
        //     }
        // }
    }

    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup not found");
        }
    }
}
