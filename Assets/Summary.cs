using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Summary : MonoBehaviour
{
    public bool debug = true; 
    public CanvasGroup CGGameplay;
    private CanvasGroup CGSummary;
    private RoundOver RoundOverCS;
    private Coins CoinsCS;
    [SerializeField] TextMeshProUGUI coinsTMP;
    private float balance;

    // Start is called before the first frame update
    void Start() {
        // Grabbing the Summary Canvas
        CGSummary = GetComponent<CanvasGroup>();
        if (CGSummary == null) {
            Debug.LogError("CGSummary not in inspector");
        }
        CGSummary.interactable = false;
        CGSummary.blocksRaycasts = false;
        CGSummary.alpha = 0f;   

        // Grabbing RoundOver.cs
        RoundOverCS = FindObjectOfType<RoundOver>();
        if (RoundOverCS == null) {
            Debug.LogError("RoundOverCS not in inspector");
        }

        // Grabbign Coins.cs
        CoinsCS = FindObjectOfType<Coins>();
        if (CoinsCS == null) {
            Debug.LogError("CoinsCS not in inspector");
        }
    }

    // Update is called once per frame
    void Update() {
        if (RoundOverCS.getPopUpDone() == true) {
            Time.timeScale = 0f;
            HandleSummary();
        } 
    }

    void HandleSummary() {
        // Static Top Text Segment

        // Coins Segment. Using Getter for Total
        float balance = CoinsCS.getBalance();
        string formattedBal = string.Format("{0:C}", balance);
        coinsTMP.text = formattedBal;

        // Customers Served Segment

        // Making Summary Appear
        CGSummary.interactable = true;
        CGSummary.blocksRaycasts = true;
        CGSummary.alpha = 1f;

        // Makes the Gameplay Overlay diappear
        CGGameplay.interactable = false;
        CGGameplay.blocksRaycasts = false;
        CGGameplay.alpha = 0f;
    }
}
