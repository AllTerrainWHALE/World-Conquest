using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class select_Player_Count_Buttons : MonoBehaviour {

    // Author: Ben Ferraz
    //
    // This Object handles the buttons of the player selection UI.
    //   - Stores the integer player count value.
    //   - A method to control the left button press.
    //   - A method to control the right button press.

    private select_Player_Count _select_Player_Count;
    private select_AI_Count _AI_Count;

    private void Start() {
        //Finds the _select_Player_Count script in the active scene so you can access its values and methods.
        _select_Player_Count = GameObject.Find("player_Count_Value_Text").GetComponent<select_Player_Count>();
        //Finds the _AI_Count script in the active scene so you can access its values and methods.
        _AI_Count = GameObject.Find("AI_Count_Value_Text").GetComponent<select_AI_Count>();
    }

    public void leftButtonPress() {
        //Peforms a check before any values are changed - if the player count integer value is greater than 0, then the left button can be pressed to decrease this value.
        //The AI count reset is to fix a bug where you could have more AI players than actual players.
        if (_select_Player_Count.playerCountValue > 2) {
            _select_Player_Count.decreasePlayerCount();
            _AI_Count.resetAICountValue();
        }
    }

    //Peforms a check before any values are changed - if the player count integer value is less than 6, then the right button can be pressed to increase this value.
    //The AI count reset is to fix a bug where you could have more AI players than actual players.
    public void rightButtonPress() {
        if (_select_Player_Count.playerCountValue < 6) {
            _select_Player_Count.increasePlayerCount();
            _AI_Count.resetAICountValue();
        }
    }
}
