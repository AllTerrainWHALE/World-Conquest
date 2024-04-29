using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_AI_Count_Buttons : MonoBehaviour {

    // Author: Ben Ferraz
    //
    // This Object handles the buttons of the AI selection UI.
    //   - Stores the integer player count value.
    //   - A method to control the left button press.
    //   - A method to control the right button press.

    private select_AI_Count _AI_Count;
    private select_Player_Count _Player_Count;

    private void Start() {
        //Finds the _AI_Count script in the active scene so you can access its values and methods.
        _AI_Count = GameObject.Find("AI_Count_Value_Text").GetComponent<select_AI_Count>();
        //Finds the _Player_Count script in the active scene so you can access its values and methods.
        _Player_Count = GameObject.Find("player_Count_Value_Text").GetComponent<select_Player_Count>();
    }

    public void leftButtonPress() {
        //Peforms a check before any values are changed - if the AI count value is a value greater than 0, then the button can be used to decrease that value.
        if (_AI_Count.AICountValue > 0) {
            _AI_Count.decreaseAICount();
        }
    }

    public void rightButtonPress() {
        //Peforms a check before any values are changed - if the AI count value is less than the player count value minus 1 then you can increase the AI count.
        //The minus 1 is to include the one actual player the game must have.
        if (_AI_Count.AICountValue < _Player_Count.playerCountValue - 1) {
            _AI_Count.increaseAICount();
        }
    }
}
