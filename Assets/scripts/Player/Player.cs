using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class Player
{

    // Identifier for each player, can be replaced with a username
    [SerializeField] int PlayerID;
    // List of tags of all currently owned regions
    [SerializeField] List<string> OwnedRegions = new List<string>();
    // The deck that the player keeps their cards in
    [SerializeField] Deck playerDeck = new Deck();
    // The bonus to be used in the setup/deployment phases
    [SerializeField] int Bonus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(int _ID) 
    {
        PlayerID = _ID;
        OwnedRegions = new List<string>();
        playerDeck = new Deck();
    }

    public List<String> getOwnedRegions()
    {
        return OwnedRegions;
    }

    public void addRegion(string newRegionTag)
    {
        OwnedRegions.Add(newRegionTag);
    }
}
