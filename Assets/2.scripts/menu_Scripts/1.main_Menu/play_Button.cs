using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class play_Button : MonoBehaviour {
    
    //Script by Ben Ferraz.

    //This script handles the play button on the main menu.



    //Fields of type list that hold game objects. This is to control menu switching.
    //disable - holds the game objects to disable when pressing the play button.
    //enable - holds the game objects to enable when pressing the play button.
    [SerializeField] private List<GameObject> disable;
    [SerializeField] private List<GameObject> enable;



    //Method that calls when the play button is pressed. "disable" is disabled and "enable" is enabled.
    public void button() {
        disable.ForEach(gameObject => { gameObject.SetActive(false); });
        enable.ForEach(gameObject => { gameObject.SetActive(true); });
    }
}
