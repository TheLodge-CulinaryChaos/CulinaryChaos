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

    [SerializeField] TextMeshProUGUI currTMP;

    // Start is called before the first frame update
    void Start() {
        current = startTime;
        started = true;
    }

    // Update is called once per frame
    void Update() {
        if (started) {
            // Update the Timer
            current -= Time.deltaTime;

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
}
