using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public class DeploymentPhase : MonoBehaviour
{
    int troopsToDeploy; // how many troops player will need to deploy
    Player player; //player whos turn now

    CardCombinationChecker CardChecker = new CardCombinationChecker();

    public void GetNewArmies()
    {
        troopsToDeploy += player.GetOwnedRegions().Count / 3; // troops for owning regions
        troopsToDeploy += player.GetBonus(); // bonus troops for owning continent
        CheckCards();
    }

    // check if there are combinations and if player have 5 or more cards
    void CheckCards()
    {
        if(CardChecker.CheckForValidCombination(player.GetPlayerDeck(), 
        out CardCombinationChecker.CardCombination combination) 
        || player.GetPlayerDeck().Count >= 5)
        {
            UseCards(combination);
        }
    }

    // method to give troops for having certain combination
    void UseCards(CardCombinationChecker.CardCombination combination)
    {
                  
    }
}
