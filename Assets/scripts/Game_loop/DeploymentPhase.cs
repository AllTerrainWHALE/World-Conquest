using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentPhase : MonoBehaviour
{
    [SerializeField] public int troopsToDeploy; // how many troops player will need to deploy
    [SerializeField] Player player; //player whos turn now
    [SerializeField] bool isItDeployment; // used on update method
    [SerializeField] OrbitalCamera cameraScript; // camera
    [SerializeField] GameObject armiesSlider; // slider for troops armies deployment

    CardCombinationChecker CardChecker = new CardCombinationChecker();
    GameLoop gameLoop = new GameLoop();

    void Update()
    {
        // when country selected and player have troops slider appears
        if(cameraScript.selectedCountry > -1 && isItDeployment && troopsToDeploy > 0)
        {
            armiesSlider.SetActive(true);
        }else{
            armiesSlider.SetActive(false);
        }
    }

    // gives troops and sets max value for slider
    // also uses cards if you 5 or more of them
    public void PhaseLoop()
    {
        if (player != null){
            GetNewArmies();
            CheckIf5Cards();
            armiesSlider.GetComponent<Slider>().maxValue = troopsToDeploy;
        }
    }

    // method for deploy button
    // gets a selected country, uses method to add troops in RegionV2 script
    // and changes max value for slider
    public void Deploy()
    {
        GameObject country = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
        country.GetComponent<RegionV2>().addTroop((int) armiesSlider.GetComponent<Slider>().value);
        troopsToDeploy -= (int) armiesSlider.GetComponent<Slider>().value;
        armiesSlider.GetComponent<Slider>().maxValue = troopsToDeploy;
    }

    // method that calculates how many troops you get
    public void GetNewArmies()
    {
        troopsToDeploy += player.GetOwnedRegions().Count / 3; // troops for owning regions
        troopsToDeploy += player.GetBonus(); // bonus troops for owning continent
    }

    public void CheckIf5Cards()
    {
        if(player.cardsOwnedByPlayer.Count >= 5)
        {
            CardChecker.CheckForValidCombination(player.cardsOwnedByPlayer, out CardCombinationChecker.CardCombination combination);
            troopsToDeploy += UseCards(combination);
        }
    }

    // method for use cards button
    // uses CardChecker script to find combination then
    // gives it to UseCards method
    public void CheckCards()
    {
        if(CardChecker.CheckForValidCombination(player.cardsOwnedByPlayer, 
        out CardCombinationChecker.CardCombination combination))
        {
            troopsToDeploy += UseCards(combination);
        }
    }

    // method to give troops for having certain combination and remove cards from player
    int UseCards(CardCombinationChecker.CardCombination combination)
    {
        // Three different types and wild card combination
        if (combination == CardCombinationChecker.CardCombination.ThreeDifferentTypes || combination == CardCombinationChecker.CardCombination.WildCardCombination)
        {
            List<CardScript.TypeOfTroops> types = new List<CardScript.TypeOfTroops>();
            int counter = 1;
            // removes cards
            for (int i = player.cardsOwnedByPlayer.Count - 1; i >= 0; i--)
            {
                Debug.Log(player.cardsOwnedByPlayer[i].typeOfTroops);
                if (!types.Contains(player.cardsOwnedByPlayer[i].typeOfTroops) && counter < 4)
                {
                    types.Add(player.cardsOwnedByPlayer[i].typeOfTroops);
                    player.cardsOwnedByPlayer.RemoveAt(i);
                    counter++;
                }
            }
        // three same types
        }else if(combination == CardCombinationChecker.CardCombination.ThreeSameTypes)
        {
            var typeCounts = new Dictionary<CardScript.TypeOfTroops, int>(); // stores how many of each type player has
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
            // removes cards
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
        
        // points formula
        if (gameLoop.cardsSetsTradedIn == 5)
        {
            return 15;
        }else if (gameLoop.cardsSetsTradedIn > 5)
        {
            return 15 + (gameLoop.cardsSetsTradedIn - 5) * 5;
        }
        return 4 + gameLoop.cardsSetsTradedIn * 2;
    }
}
