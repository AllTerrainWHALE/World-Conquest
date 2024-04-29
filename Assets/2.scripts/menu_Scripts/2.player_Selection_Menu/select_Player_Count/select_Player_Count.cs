using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class select_Player_Count : MonoBehaviour {

    // Author: Ben Ferraz
    //
    // This Object handles the values of the player selection UI.
    //   - Stores the integer player count value.
    //   - A method to increase the player count value.
    //   - A method to decrease the player count value.
    //   - A method to retrieve the player count value.

    public int playerCountValue;
    private TextMeshProUGUI playerCountValueUi;

    private void Start() {
        playerCountValueUi = GetComponent<TextMeshProUGUI>();

        playerCountValue = 2;

        //Sets the UI to display the integer player count value.
        playerCountValueUi.text = playerCountValue.ToString();  
    }

    public void increasePlayerCount() {
        //Increases the integer player count value.
        playerCountValue++;
        //Updates the UI to display the most recent change in the integer player count value.
        playerCountValueUi.text = playerCountValue.ToString();
    }

    public void decreasePlayerCount() {
        //Decreases the integer AI count value.
        playerCountValue--;
        //Updates the UI to display the most recent change in the integer player count value.
        playerCountValueUi.text = playerCountValue.ToString();
    }

    public int getPlayerCountValue() {
        //Returns the integer player count value.
        return playerCountValue;
    }
}

