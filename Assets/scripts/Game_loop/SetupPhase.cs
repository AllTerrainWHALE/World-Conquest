using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SetupPhase
{

    private int turnCounter;
    private int regionCounter;
    private int limit;
    [SerializeField] int numOfTroopsToPlace;
    private List<Player> playerList;
    [SerializeField] GameObject Gameloop;
    [SerializeField] GameObject camera_;
    [SerializeField] GameLoop gameLoopScript;
    [SerializeField] OrbitalCamera cameraScript;

    //Author: Eoin
    //
    // Initialises object
    public void Init(List<Player> _playerList)
    {
        Gameloop = GameObject.FindGameObjectWithTag("Game_Loop");
        gameLoopScript = Gameloop.GetComponent<GameLoop>();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
        cameraScript = camera_.GetComponent<OrbitalCamera>();

        //getting number of players

        playerList = _playerList;

        numOfTroopsToPlace = 40 - ((playerList.Count() - 2) * 5);

        limit = numOfTroopsToPlace * playerList.Count();
    }

    //Author: Eoin
    public void PhaseLoop()
    {

        //get current player

        if(cameraScript.selectedCountry != -99){

            int currentPlayerIndex = turnCounter % playerList.Count();

            GameObject currentlySelectedRegion = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
            RegionV2 currentlySelectedRegionScript = currentlySelectedRegion.GetComponent<RegionV2>();

            if(turnCounter < 42){
                // stage in the setup where players choose regions
                
                if(ValidateCountrySelection_picking(cameraScript.selectedCountryTag)){
                    // selected region valid
                    // (not already owned)

                    Component[] components = currentlySelectedRegion.GetComponents(typeof(Component));
                    foreach(Component component in components) {
                        Debug.Log("Testing: "+ component.ToString());
                    }

                    Debug.Log("(1)Selected country: "+ cameraScript.selectedCountryTag);
                    Debug.Log("(1)Current player:"+ currentPlayerIndex +" "+ playerList[currentPlayerIndex]);
                    Debug.Log("(1)Selected Country Script: "+currentlySelectedRegionScript);

                    currentlySelectedRegionScript.addTroop();
                    playerList[currentPlayerIndex].addRegion(cameraScript.selectedCountryTag);
                    turnCounter += 1;

                } else {
                    // selected region invalid

                    Debug.Log("(2)Selected country: "+ cameraScript.selectedCountryTag);
                    Debug.Log("(2)Selected Country Script: "+currentlySelectedRegionScript);

                    cameraScript.selectedCountry = -99;
                    currentlySelectedRegionScript.selectRegion();

                }
            } else {
                // stage in the setup where players place remaining troops

                if(ValidateRegionSelection_reinforcing(cameraScript.selectedCountryTag, currentPlayerIndex)) {
                    // region selected is valid
                    // (owned by the current player)

                    currentlySelectedRegionScript.addTroop();
                    playerList[currentPlayerIndex].addRegion(cameraScript.selectedCountryTag);
                    turnCounter += 1;

                } else {
                    // region selected is invalid

                    cameraScript.selectedCountry = -99;
                    currentlySelectedRegionScript.selectRegion();

                }
            }
        }

        if(turnCounter == limit) {
            // move onto the next phase
            gameLoopScript.incrementPhase();
        }
        
    }

    //Author: Eoin
    //
    // Selection validator for the stage where players choose a region
    private bool ValidateCountrySelection_picking(string regionTag)
    {

        //check if region unowned

        foreach(Player p in playerList) {
            if(p.getOwnedRegions().Contains(regionTag)) {return false;}
        }

        return true;
    }

    //Author: Eoin
    //
    // Selection validator for the stage where players add remaining troops to the region
    private bool ValidateRegionSelection_reinforcing(string regionTag, int currentPlayerIndex)
    {

        //check if region owned by the current player

        Player currentPlayer = playerList[currentPlayerIndex];
        if( !(currentPlayer.getOwnedRegions().Contains(regionTag)) ) { return false; }

        return true;
    }

}
