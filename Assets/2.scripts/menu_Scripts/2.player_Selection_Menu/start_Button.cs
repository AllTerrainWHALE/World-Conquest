using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_Button : MonoBehaviour {

    // Author: Ben Ferraz
    //
    // This Object handles start button.
    //   - A method that runs when the start button is pressed.

    private select_Player_Count select_Player_Count;
    private select_AI_Count select_AI_Count;

    private void Start() {
        //Finds the _select_Player_Count script in the active scene so you can access its values and methods.
        select_Player_Count = GameObject.Find("player_Count_Value_Text").GetComponent<select_Player_Count>();
        //Finds the _AI_Count script in the active scene so you can access its values and methods.
        select_AI_Count = GameObject.Find("AI_Count_Value_Text").GetComponent<select_AI_Count>();
    }

    public void startButton(){
        //Saves the player count integer value into the player prefs "PlayerCount" field.
        PlayerPrefs.SetInt("PlayerCount", select_Player_Count.playerCountValue);
        //Saves the AI count count integer value into the player prefs "AICount" field.
        PlayerPrefs.SetInt("AICount", select_AI_Count.AICountValue);
        //Saves both of the values into player prefs.
        PlayerPrefs.Save();
        //Loads the main game scene.
        SceneManager.LoadScene(1);
    }

}
