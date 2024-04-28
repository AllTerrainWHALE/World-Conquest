using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Prefs_Test : MonoBehaviour {

    int playerAmountValue;
    int aiAmountValue;

    private void Start() {
        playerAmountValue = PlayerPrefs.GetInt("PlayerCount", 0);
        aiAmountValue = PlayerPrefs.GetInt("AICount", 0);
    }

    public void buttonPress() {
        Debug.Log("The selected player count is: " +  playerAmountValue);
        Debug.Log("The selected ai count is: " + aiAmountValue);
    }

}
