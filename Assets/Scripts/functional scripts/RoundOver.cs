using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundOver : MonoBehaviour
{
    private CanvasGroup CGRoundOver;
    private Timer timerCS;
    private float PopUpLength;
    private bool PopUpDone;

    // Start is called before the first frame update
    void Start() {        
        PopUpLength = 3.0f;
        PopUpDone = false;
        Time.timeScale = 1f;

        CGRoundOver = GetComponent<CanvasGroup>();
        if (CGRoundOver == null) {
            // Debug.LogError("CGRoundOver not in inspector");
        }
        CGRoundOver.interactable = false;
        CGRoundOver.blocksRaycasts = false;
        CGRoundOver.alpha = 0f;

        timerCS = FindObjectOfType<Timer>();
        if (timerCS == null) {
            // Debug.LogError("timerCS not in inspector");
        }
    }

    // Update is called once per frame
    void Update() {
        if (timerCS.getCurrent() <= 0.0) {
            HandleTimerOver();
        } else {
            PopUpDone = false;
        }
    }

    void HandleTimerOver() {
        // If The Pop Up Timer Reaches Zero, it disappears and makes
        // the PopUpDone bool true. Summary.cs checks that thru getPopUpDone
        // to see if that panel pops up
        if (PopUpLength <= 0.0f) {
            PopUpLength = 0f;
            CGRoundOver.interactable = false;
            CGRoundOver.blocksRaycasts = false;
            CGRoundOver.alpha = 0f;
            PopUpDone = true;
            return;
        } else { // Else decrement the PopUpLength and keep it visible
            PopUpLength -= Time.deltaTime;
            CGRoundOver.interactable = true;
            CGRoundOver.blocksRaycasts = true;
            CGRoundOver.alpha = 1f;
        }
    }

// Getter to see the value of PopUpDone
    public bool getPopUpDone() {
        return PopUpDone;
    }
}

