using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Default player names to randomly select from
    public static List<string> defaultPlayerNames = new()
    {
        "Ben", "Bradley", "Eoin", "Harvey", "Ilja", "Wei",
        "Kingsley Sage", "Quentin Raffles", "Jack Speat", "Tom Dent"
    };

    // Identifier for each player, can be replaced with a username
    [SerializeField] int PlayerID;
    // Identifies which team the player is on
    [SerializeField] TroopColour troopTeam;
    public TroopColour team => troopTeam;
    // List of tags of all currently owned regions
    [SerializeField] List<string> OwnedRegions = new List<string>();
    // The deck that the player keeps their cards in
    [SerializeField] public List<CardScript> playerDeck; // Ilja: I think someone created temporary empty card scripts, but now we have finished ones
    // The bonus to be used in the setup/deployment phases
    [SerializeField] int bonus;

    private GameObject playerDeckContainer => GameObject.Find("player_Deck_Container");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int _ID, TroopColour _troopTeam)
    {
        PlayerID = _ID;
        troopTeam = _troopTeam;
        OwnedRegions = new List<string>();
        playerDeck = new List<CardScript>();
    }

    public List<String> GetOwnedRegions()
    {
        return OwnedRegions;
    }

    // Author: Bradley & Harvey
    public bool isOwnedRegion(string regionTag) => OwnedRegions.Contains(regionTag);

    public void addRegion(string newRegionTag) => OwnedRegions.Add(newRegionTag);
    public void removeRegion(string regionTag) => OwnedRegions.Remove(regionTag);
    
    public int GetBonus() => bonus;
    public void SetBonus(int newBonus) => bonus = newBonus;

    public void UpdateCards(List<CardScript> cards) => playerDeck = cards;
    public void GiveCard(CardScript card) => playerDeck.Add(card);

    public void DisplayCards() => playerDeck.ForEach(c => Instantiate(c, playerDeckContainer.transform));
    public void HideCards()
    {
        for (int i = 0; i < playerDeckContainer.transform.childCount; i++)
            Destroy(playerDeckContainer.transform.GetChild(i).gameObject);
        Debug.Log("Wooo?");
    }
    public void RefreshCardDisplay() { HideCards(); DisplayCards(); }
}
