using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{
    // inits
    public float balance;
    public bool debug = true;

    // level : minimum coins
    public static Dictionary<int, int> levelsToCoins = new Dictionary<int, int>
    {
        { 1, 600 },
        { 2, 700 },
        { 3, 800 },
    };

    [SerializeField] TextMeshProUGUI currTMP;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string formattedBal = $"{balance}";
        currTMP.text = formattedBal;
        if (debug && Input.GetKeyDown(KeyCode.U))
        {
            balance += .24f;
        }
    }

    // Allows you to add Money to the Scoreboard
    public void addMoney(float money)
    {
        balance += money;
    }

    // Getter to see current Balance
    public float getBalance()
    {
        return balance;
    }
}
