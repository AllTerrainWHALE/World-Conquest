using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class select_AI_Count : MonoBehaviour {

    // Author: Ben Ferraz
    //
    // This Object handles the values of the AI selection UI.
    //   - Stores the integer AI count value.
    //   - A method to increase the AI count value.
    //   - A method to decrease the AI count value.
    //   - A method to reset the AI count value.
    //   - A method to retrieve the AI count value.

    public int AICountValue;
    private TextMeshProUGUI AICountValueUi;

    private void Start(){
        AICountValueUi = GetComponent<TextMeshProUGUI>();

        //Sets the UI to display the integer AI count value.
        AICountValueUi.text = AICountValue.ToString();
    }

    public void increaseAICount(){
        //Increases the integer AI count value.
        AICountValue++;
        //Updates the UI to display the most recent change in the integer AI count value.
        AICountValueUi.text = AICountValue.ToString();
    }

    public void decreaseAICount(){
        //Decreases the integer AI count value.
        AICountValue--;
        //Updates the UI to display the most recent change in the integer AI count value.
        AICountValueUi.text = AICountValue.ToString();
    }

    public void resetAICountValue(){
        //Sets the integer AI count value to zero.
        AICountValue = 0;
        //Updates the UI to display the most recent change in the integer AI count value.
        AICountValueUi.text = AICountValue.ToString();
    }

    public int getAICountValue(){
        //Returns the integer AI count value.
        return AICountValue;
    }
}
