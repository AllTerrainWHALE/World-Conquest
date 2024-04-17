using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SetupPhase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PhaseLoop()
    {
        GameObject Gameloop = GameObject.FindGameObjectWithTag("Gameloop");
        GameLoop gameLoopScript = Gameloop.GetComponent<GameLoop>();
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        OrbitalCamera cameraScript = camera.GetComponent<OrbitalCamera>();

        //getting number of players

        List<Player> playerList = gameLoopScript.GetPlayerList();

        int numOfTroopsToPlace = 40 - ((playerList.Count() - 2) * 5);

        int turnCounter = 0;
        int limit = numOfTroopsToPlace * 42;
        int regionCounter = 0;

        while (turnCounter < limit)
        {
            int currentPlayerIndex = turnCounter % playerList.Count();
            Player currentPlayer = playerList[currentPlayerIndex];

            while (true)
            {
                if (cameraScript.selectedCountry != -99)
                {
                    GameObject selectedRegion = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag);
                    if (regionCounter == 42)
                    //checks if all regions have been claimed
                    {
                        if(currentPlayer.getOwnedRegions().Contains(cameraScript.selectedCountryTag))
                        //checks if the current player owns the region
                        {
                            GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>().addTroop();
                            currentPlayer.addRegion(cameraScript.selectedCountryTag);
                            turnCounter += 1;
                            break;
                        }
                    }
                    else 
                    {
                        //if this is true then the region is owned by someone
                        bool ownershipFlag = false;
                        foreach(Player p in playerList)
                        {
                            if(p.getOwnedRegions().Contains(cameraScript.selectedCountryTag))
                            {
                                ownershipFlag = true;
                                break;
                            }
                        }

                        if (ownershipFlag == false)
                        {
                            GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>().addTroop();
                            currentPlayer.addRegion(cameraScript.selectedCountryTag);
                            regionCounter += 1;
                            turnCounter += 1;
                            break;
                        }
                    }
                }
            }
        }

    }
}
