using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour {
    // inits
    public float startTime = 120;
    float current;
    public bool started = false;
    string formattedTime;
    // Debug
    public bool debug;
    

    [SerializeField] TextMeshProUGUI currTMP;

    // Start is called before the first frame update
    void Start() {
        current = startTime;

        if (debug) {
            current = 5.0f;
        }
        started = true;
    }

    // Update is called once per frame
    void Update() {
        if (started) {
            // Update the Timer
            if (!debug) {
            current -= Time.deltaTime;
            }

            if (current <= 0) {
                // Set Current to 0 and Stop Visual for Countdown
                current = 0;
                started = false;
                formattedTime = "Level Complete";
            } else {
                // Format it so it looks good
                int currMin = Mathf.FloorToInt(current / 60);
                int currSec = Mathf.FloorToInt(current % 60);
                formattedTime = string.Format("{0:00}:{1:00}", currMin, currSec);
            }

            // Send to Unity
            currTMP.text = formattedTime;
        } 
    }


    // Getter to see current. Current is the remaining time in the round
    public float getCurrent() {
        return current;
    }
}
