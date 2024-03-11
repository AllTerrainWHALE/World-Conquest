using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Continent : MonoBehaviour
{

    // Tag of the continent
    [SerializeField] String continentTag;
    // Set containing all players who own regions within the continent
    // When set length reaches one, a player owns all regions
    [SerializeField] HashSet<int> playersInContinent = new HashSet<int>();
    // The bonus for owning the entire continent
    [SerializeField] int bonus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Author: 
    // 
    // Method to assign continent bonus
    private void giveBonus()
    {
        
    }

    // Author:
    //
    // Adds a player to the set
    private void addPlayer()
    {

    }

    // Author: 
    //
    // Removes a player from the set
    private void removePlayer()
    {

    }

}
