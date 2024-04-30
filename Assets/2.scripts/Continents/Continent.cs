using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class Continent : MonoBehaviour
{

    // Tag of the continent
    [SerializeField] String continentTag => this.tag;
    // List of all regions with the continent
    [SerializeField] List<GameObject> regionObjects;

    // Set containing all players who own regions within the continent
    // When set length reaches one, a player owns all regions
    [SerializeField] public List<Player> playersInContinent => regionObjects
        .Select(r => r.GetComponent<RegionV2>().GetRulingPlayer())
        .Distinct().Where(p => p != null).ToList();

    // The bonus for owning the entire continent
    [SerializeField] public int bonus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PlayerRulesContinent(Player player) =>
        playersInContinent.Count() == 1 && playersInContinent[0] == player;

}