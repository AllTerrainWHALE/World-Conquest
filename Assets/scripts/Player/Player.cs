using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Identifier for each player, can be replaced with a username
    [SerializeField] int PlayerID;
    // List of tags of all currently owned regions
    [SerializeField] List<string> OwnedRegions;
    // The deck that the player keeps their cards in
    [SerializeField] public List<CardScript> cardsOwnedByPlayer;
    // The bonus to be used in the setup/deployment phases
    [SerializeField] int bonus;
    [SerializeField] public int cardsSetsTradedIn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<string> GetOwnedRegions() { return OwnedRegions; }
    public int GetBonus() { return bonus; }
    public void UpdateCards(List<CardScript> cards) { cardsOwnedByPlayer = cards; }
}
