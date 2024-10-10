using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour {
    // inits
    public float balance;
    public bool debug = false;
    [SerializeField] TextMeshProUGUI currTMP;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        string formattedBal = string.Format("{0:C}", balance);
        currTMP.text = formattedBal;
        debugMoney();
    }

    public void addMoney(float money) {
        balance += money;
    }

    public void debugMoney() {
        if (debug && Input.GetKeyUp(KeyCode.U)) {
            balance += .24f;
        }
    }
}
