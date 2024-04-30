using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.UI;

public class DeploymentPhase : MonoBehaviour
{
    [SerializeField] public int troopsToDeploy; // how many troops player will need to deploy
    [SerializeField] Player player; //player whos turn now
    [SerializeField] GameLoop gameLoop;
    [SerializeField] OrbitalCamera cameraScript; // camera

    [Header("Troop Selection & Deployment")]
    [SerializeField] GameObject armiesSlider; // slider for troops armies deployment
    private SliderController armiesSliderScript => // slider for troops armies deployment
        armiesSlider.GetComponent<SliderController>();
    [SerializeField] GameObject armiesDeployButton;

    [Header("")]
    [SerializeField] int subPhaseNumber = 0;

    CardCombinationChecker CardChecker = new CardCombinationChecker();

    void Start()
    {

    }

    void Update()
    {

    }

    // gives troops and sets max value for slider
    // also uses cards if you 5 or more of them
    public void PhaseLoop(Player _player)
    {
        player = _player;

        switch (subPhaseNumber)
        {
            case 0:
                if (player != null)
                {
                    GetNewArmies();
                    CheckIf5Cards();
                    armiesSliderScript.maxValue = troopsToDeploy;
                }
                else throw new System.Exception("Player object undefinded");
                subPhaseNumber += 1;
                break;

            case 1:
                // Author: Bradley (just the one line) | Highlight all owned countries
                player.GetOwnedRegions().ForEach(r => GameObject.FindGameObjectWithTag(r).GetComponent<RegionV2>().isHighlighted = true);

                // when country selected and player have troops slider appears
                if (cameraScript.selectedCountry > -1 && troopsToDeploy > 0 && player.isOwnedRegion(cameraScript.selectedCountryTag))
                {
                    armiesSlider.SetActive(true);
                    armiesDeployButton.SetActive(true);
                }
                else
                {
                    armiesSlider.SetActive(false);
                    armiesDeployButton.SetActive(false);

                    cameraScript.selectedCountry = -99;
                }

                if (troopsToDeploy <= 0)
                {
                    subPhaseNumber = 0;
                    gameLoop.incrementPhase();
                }

                break;
        }
    }

    // method for deploy button
    // gets a selected country, uses method to add troops in RegionV2 script
    // and changes max value for slider
    public void Deploy()
    {
        GameObject country = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
        country.GetComponent<RegionV2>().addTroop(player.team, (int) armiesSliderScript.value);
        troopsToDeploy -= (int) armiesSliderScript.value;
        armiesSliderScript.maxValue = troopsToDeploy;
    }

    // method that calculates how many troops you get
    public void GetNewArmies()
    {
        troopsToDeploy += player.GetOwnedRegions().Count / 3; // troops for owning regions
        troopsToDeploy += player.GetBonus(); // bonus troops for owning continent
    }

    public void CheckIf5Cards()
    {
        if(player.playerDeck.Count >= 5)
        {
            CardChecker.CheckForValidCombination(player.playerDeck, out CardCombinationChecker.CardCombination combination);
            troopsToDeploy += UseCards(combination);
        }
    }

    // method for use cards button
    // uses CardChecker script to find combination then
    // gives it to UseCards method
    public void CheckCards()
    {
        if(CardChecker.CheckForValidCombination(player.playerDeck, 
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
            for (int i = player.playerDeck.Count - 1; i >= 0; i--)
            {
                Debug.Log(player.playerDeck[i].typeOfTroops);
                if (!types.Contains(player.playerDeck[i].typeOfTroops) && counter < 4)
                {
                    types.Add(player.playerDeck[i].typeOfTroops);
                    player.playerDeck.RemoveAt(i);
                    counter++;
                }
            }
        // three same types
        }else if(combination == CardCombinationChecker.CardCombination.ThreeSameTypes)
        {
            var typeCounts = new Dictionary<CardScript.TypeOfTroops, int>(); // stores how many of each type player has
            foreach (var card in player.playerDeck)
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
                for (int i = player.playerDeck.Count - 1; i >= 0 && typeCounts[type] > 0; i--)
                {
                    if (player.playerDeck[i].typeOfTroops == type)
                    {
                        player.playerDeck.RemoveAt(i);
                        typeCounts[type]--;
                    }
                }
            }
        }
        player.UpdateCards(player.playerDeck);
        
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
