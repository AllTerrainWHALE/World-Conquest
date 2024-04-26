using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCombinationChecker : MonoBehaviour
{
    // combinations
    public enum CardCombination
    {
        ThreeDifferentTypes,
        ThreeSameTypes,
        WildCardCombination
    }

    // method to check for valid card combinations
    public bool CheckForValidCombination(List<CardScript> playerCards, out CardCombination combination)
    {

        int infantryCount = 0;
        int artilleryCount = 0;
        int cavalryCount = 0;
        int wildCardCount = 0;

        // checks through players cards and counts them
        foreach (CardScript card in playerCards)
        {
            switch (card.typeOfTroops)
            {
                case CardScript.TypeOfTroops.Infatry:
                    infantryCount++;
                    break;
                case CardScript.TypeOfTroops.Artillery:
                    artilleryCount++;
                    break;
                case CardScript.TypeOfTroops.Cavalry:
                    cavalryCount++;
                    break;
                case CardScript.TypeOfTroops.WildCard:
                    wildCardCount++;
                    break;
            }
        }

        // Check for different types of cards
        if (infantryCount >= 1 && artilleryCount >= 1 && cavalryCount >= 1)
        {
            combination = CardCombination.ThreeDifferentTypes;
            return true;
        }

        // Check for three of the same type
        if (infantryCount >= 3 || artilleryCount >= 3 || cavalryCount >= 3)
        {
            combination = CardCombination.ThreeSameTypes;
            return true;
        }

        // Check for a combination with wild cards
        if ((infantryCount >= 1 && artilleryCount >= 1 && wildCardCount >= 1) ||
            (infantryCount >= 1 && cavalryCount >= 1 && wildCardCount >= 1) ||
            (artilleryCount >= 1 && cavalryCount >= 1 && wildCardCount >= 1))
        {
            combination = CardCombination.WildCardCombination;
            return true;
        }

        combination = CardCombination.ThreeDifferentTypes;
        return false;
    }
}
