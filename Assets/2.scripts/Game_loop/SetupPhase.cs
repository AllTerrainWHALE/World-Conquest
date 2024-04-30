using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SetupPhase : MonoBehaviour
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

    [Header("UI stuff")]
    //[SerializeField] GameObject phaseNumberUI;
    [SerializeField] GameObject playerTurnCounterUI;
    //[SerializeField] GameObject troopSliderUI;
    //[SerializeField] GameObject troopSliderScript;

    //Author: Eoin
    //
    // Initialises object
    public void Init(List<Player> _playerList)
    {
        Gameloop = GameObject.FindGameObjectWithTag("Game_Loop");
        gameLoopScript = Gameloop.GetComponent<GameLoop>();
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
        cameraScript = camera_.GetComponent<OrbitalCamera>();

        //troopsSliderScript = troopsSliderUI.GetComponent<SliderController>();

        //getting UI bits

        //phaseNumberUI = GameObject.FindGameObjectWithTag("PhaseNameUI");

        //getting number of players

        playerList = _playerList;

        numOfTroopsToPlace = 40 - ((playerList.Count() - 2) * 5);

        limit = numOfTroopsToPlace * playerList.Count();
    }

    //Author: Eoin
    public void PhaseLoop()
    {
        int currentPlayerIndex = turnCounter % playerList.Count();

        //Debug.Log(phaseNumberUI);
        //Debug.Log(phaseNumberUI.GetComponents(typeof(TextMeshPro)));
        //TextMeshPro textMesh = phaseNumberUI.GetComponent<TextMeshPro>();
        //Debug.Log(textMesh);
        //Debug.Log(textMesh.text);
        //textMesh.text = "Setup Phase";

        //get current player

        // Author: Bradley (just the one line) | Highlight all unclaimed countries (I know, it's disgusting)
        if (turnCounter < 42)
        {
            // Get all GameObject tags, and filter out only those assigned to countries
            List<string> allTags = UnityEditorInternal.InternalEditorUtility.tags.ToList();
            GameObject gameObject;
            RegionV2 countryScript;
            foreach (string tag in allTags)
            {
                // Check if the tag is assigned to an object
                gameObject = GameObject.FindGameObjectWithTag(tag);
                if (gameObject == null) continue;

                // Check if verified object holds a RegionV2 script (confirm it's a country)
                countryScript = gameObject.GetComponent<RegionV2>();
                if (countryScript == null) continue;

                // Highlight verified country
                countryScript.isHighlighted = true;
            }

            // Disable highlighting for claimed countries
            playerList.ForEach(p => p
                .GetOwnedRegions()
                .ForEach(r => GameObject
                    .FindGameObjectWithTag(r)
                    .GetComponent<RegionV2>().isHighlighted = false
                    )
                );
        }
        else playerList[currentPlayerIndex].GetOwnedRegions().ForEach(r => GameObject.FindGameObjectWithTag(r).GetComponent<RegionV2>().isHighlighted = true);
        // End Author: Bradley

        if (cameraScript.selectedCountry != -99){

            TMP_Text playerText = playerTurnCounterUI.GetComponent<TMP_Text>();
            playerText.text = "Player " + (currentPlayerIndex + 1);

            GameObject currentlySelectedRegion = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
            RegionV2 currentlySelectedRegionScript = currentlySelectedRegion.GetComponent<RegionV2>();

            if(turnCounter < 42){
                // stage in the setup where players choose regions

                if (ValidateCountrySelection_picking(cameraScript.selectedCountryTag)){
                    // selected region valid
                    // (not already owned)

                    Component[] components = currentlySelectedRegion.GetComponents(typeof(Component));
                    foreach(Component component in components) {
                        Debug.Log("Testing: "+ component.ToString());
                    }

                    Debug.Log("(1)Selected country: "+ cameraScript.selectedCountryTag);
                    Debug.Log("(1)Current player:"+ currentPlayerIndex +" "+ playerList[currentPlayerIndex]);
                    Debug.Log("(1)Selected Country Script: "+currentlySelectedRegionScript);

                    currentlySelectedRegionScript.addTroop(playerList[currentPlayerIndex].team);
                    playerList[currentPlayerIndex].addRegion(cameraScript.selectedCountryTag);
                    currentlySelectedRegionScript.SetRulingPlayer(playerList[currentPlayerIndex]);
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

                if (ValidateRegionSelection_reinforcing(cameraScript.selectedCountryTag, currentPlayerIndex)) {
                    // region selected is valid
                    // (owned by the current player)

                    currentlySelectedRegionScript.addTroop(playerList[currentPlayerIndex].team);
                    //playerList[currentPlayerIndex].addRegion(cameraScript.selectedCountryTag);
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
            if(p.GetOwnedRegions().Contains(regionTag)) return false;
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
        if( !(currentPlayer.GetOwnedRegions().Contains(regionTag)) ) { return false; }

        return true;
    }

    private void updateUI(int PlayerNum, int numOfTroopsLeftToPlace) {



    }

}
