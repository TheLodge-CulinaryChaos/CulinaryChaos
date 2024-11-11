using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    public GameObject theBoy;
    public GameObject theGirl;
    
    // make a public selection variable
    public static Enums selectedPlayer = Enums.THE_BOY;

    public bool debug = false;
    public Enums debugSelectedPlayer = Enums.THE_BOY;

    void Awake() {
        // set player by selectedPlayerIndex
        if (debug) {
            selectedPlayer = debugSelectedPlayer;
        }
        SetPlayer(selectedPlayer);
    }

    public GameObject GetSelectedPlayer() {
        return GetPlayer(selectedPlayer);
    }

    public GameObject GetPlayer(Enums player) {
        switch (player) {
            case Enums.THE_BOY:
                return theBoy;
            case Enums.THE_GIRL:
                return theGirl;
        }
        throw new System.Exception("Invalid player.");
    }

    public void SetPlayer(Enums player) {
        theBoy.SetActive(false);
        theGirl.SetActive(false);
        GetPlayer(player).SetActive(true);
        selectedPlayer = player;
    }
}
