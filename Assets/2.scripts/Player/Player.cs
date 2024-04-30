using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Player : MonoBehaviour
{

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

    public void UpdateCards(List<CardScript> cards) { playerDeck = cards; }
}
