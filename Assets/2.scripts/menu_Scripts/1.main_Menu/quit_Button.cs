using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quit_Button : MonoBehaviour {
    
    //Script by Ben Ferraz.

    //This script handles the quit button on the main menu.



    //Method that calls when the quit button is pressed.
    //When pressed the application is closed.
    public void button() {
        Debug.Log("Quits the application when it's built!");
        Application.Quit();
    }
}
