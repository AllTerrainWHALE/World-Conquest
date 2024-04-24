using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentPhase : MonoBehaviour
{
    [SerializeField] public int troopsToDeploy; // how many troops player will need to deploy
    [SerializeField] Player player; //player whos turn now
    [SerializeField] bool isItDeployment;
    [SerializeField] OrbitalCamera cameraScript;
    [SerializeField] GameObject armiesSlider;

    CardCombinationChecker CardChecker = new CardCombinationChecker();

    void Update()
    {

        if(cameraScript.selectedCountry > -1 && isItDeployment && troopsToDeploy > 0)
        {
            armiesSlider.SetActive(true);
        }else{
            armiesSlider.SetActive(false);
        }
    }

    public void PhaseLoop()
    {
        if (player != null){
            GetNewArmies();
            CheckCards();
            armiesSlider.GetComponent<Slider>().maxValue = troopsToDeploy;
        }
    }

    public void Deploy()
    {
        Debug.Log(cameraScript.selectedCountryTag);
        GameObject country = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
        country.GetComponent<RegionV2>().addTroop((int) armiesSlider.GetComponent<Slider>().value);
        troopsToDeploy -= (int) armiesSlider.GetComponent<Slider>().value;
        armiesSlider.GetComponent<Slider>().maxValue = troopsToDeploy;
        Debug.Log(armiesSlider.GetComponent<Slider>().value + " armies deployed in " + country.tag);
    }

    public void GetNewArmies()
    {
        troopsToDeploy += player.GetOwnedRegions().Count / 3; // troops for owning regions
        troopsToDeploy += player.GetBonus(); // bonus troops for owning continent
    }

    // check if there are combinations and if player have 5 or more cards
    public void CheckCards()
    {
        if(CardChecker.CheckForValidCombination(player.cardsOwnedByPlayer, 
        out CardCombinationChecker.CardCombination combination) 
        || player.cardsOwnedByPlayer.Count >= 5)
        {
            Debug.Log(combination);
            troopsToDeploy += UseCards(combination);
        }
    }

    // method to give troops for having certain combination and remove cards from player
    int UseCards(CardCombinationChecker.CardCombination combination)
    {
        Debug.Log(player.cardsOwnedByPlayer);
        if (combination == CardCombinationChecker.CardCombination.ThreeDifferentTypes || combination == CardCombinationChecker.CardCombination.WildCardCombination)
        {
            List<CardScript.TypeOfTroops> types = new List<CardScript.TypeOfTroops>();
            int counter = 1;
            for (int i = player.cardsOwnedByPlayer.Count - 1; i >= 0; i--)
            {
                Debug.Log(player.cardsOwnedByPlayer[i].typeOfTroops);
                if (!types.Contains(player.cardsOwnedByPlayer[i].typeOfTroops) && counter < 4)
                {
                    types.Add(player.cardsOwnedByPlayer[i].typeOfTroops);
                    player.cardsOwnedByPlayer.RemoveAt(i);
                    counter++;
                }
                Debug.Log("i: " + i);
                Debug.Log("Counter: " + counter);
            }
        }else if(combination == CardCombinationChecker.CardCombination.ThreeSameTypes)
        {
            var typeCounts = new Dictionary<CardScript.TypeOfTroops, int>();
            foreach (var card in player.cardsOwnedByPlayer)
            {
                if (typeCounts.ContainsKey(card.typeOfTroops))
                {
                    typeCounts[card.typeOfTroops]++;
                }
                else
                {
                    typeCounts[card.typeOfTroops] = 1;
                }
            }
            foreach (var type in typeCounts.Keys)
            {
                for (int i = player.cardsOwnedByPlayer.Count - 1; i >= 0 && typeCounts[type] > 0; i--)
                {
                    if (player.cardsOwnedByPlayer[i].typeOfTroops == type)
                    {
                        player.cardsOwnedByPlayer.RemoveAt(i);
                        typeCounts[type]--;
                    }
                }
            }
        }
        player.UpdateCards(player.cardsOwnedByPlayer);
        if (player.cardsSetsTradedIn == 5)
        {
            return 15;
        }else if (player.cardsSetsTradedIn > 5)
        {
            return 15 + (player.cardsSetsTradedIn - 5) * 5;
        }
        return 4 + player.cardsSetsTradedIn * 2;
    }
}
