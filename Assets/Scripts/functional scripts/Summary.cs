using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Summary : MonoBehaviour
{
    public bool debug = true;
    public CanvasGroup CGGameplay;
    public int level;
    private CanvasGroup CGSummary;
    private RoundOver RoundOverCS;
    private Coins CoinsCS;
    [SerializeField] TextMeshProUGUI roundStatusTMP;
    [SerializeField] TextMeshProUGUI miniumScoreTMP;
    [SerializeField] TextMeshProUGUI coinsTMP;
    [SerializeField] TextMeshProUGUI customersTMP;
    private float balance;

    // Start is called before the first frame update
    void Start()
    {
        // Grabbing the Summary Canvas
        CGSummary = GetComponent<CanvasGroup>();
        if (CGSummary == null)
        {
            // Debug.LogError("CGSummary not in inspector");
        }
        CGSummary.interactable = false;
        CGSummary.blocksRaycasts = false;
        CGSummary.alpha = 0f;

        // Grabbing RoundOver.cs
        RoundOverCS = FindObjectOfType<RoundOver>();
        if (RoundOverCS == null)
        {
            // Debug.LogError("RoundOverCS not in inspector");
        }

        // Grabbign Coins.cs
        CoinsCS = FindObjectOfType<Coins>();
        if (CoinsCS == null)
        {
            // Debug.LogError("CoinsCS not in inspector");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundOverCS.getPopUpDone() == true)
        {
            Time.timeScale = 0f;
            HandleSummary();
        }
    }

    void HandleSummary()
    {
        // Static Top Text Segment

        // Coins Segment. Using Getter for Total
        float balance = CoinsCS.getBalance();
        coinsTMP.text = $"Money Acquired: {balance}";

        miniumScoreTMP.text = $"Minimum Score: {Coins.levelsToCoins[level]}";

        NPCManager npcManager = FindObjectOfType<NPCManager>();
        customersTMP.text = $"Customers Served: {npcManager.GetServedCustomers()}";

        // Round Status Segment
        if (balance >= Coins.levelsToCoins[level])
        {
            roundStatusTMP.text = "You Meet the Minimum Score! Good Job!";
            // set Text to Green
            roundStatusTMP.color = new Color(0, 1, 0);
        }
        else
        {
            roundStatusTMP.text = "You Did Not Meet the Minimum Score. Try Again!";
            // set Text to Red
            roundStatusTMP.color = new Color(1, 0, 0);
        }

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
