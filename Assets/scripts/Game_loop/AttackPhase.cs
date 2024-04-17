using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPhase : MonoBehaviour
{
    private RegionV2 attackingCountry;
    private RegionV2 defendingCountry;

    // 0 = country selection | 1 = dice rolling
    private int subPhaseNumber;

    [SerializeField] OrbitalCamera cameraScript;


    // Start is called before the first frame update
    void Start()
    {
       // cameraScript = GetComponent<Camera>().GetComponent<OrbitalCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Author: 
    public void PhaseLoop(Player currentPlayer)
    {
        switch (subPhaseNumber)
        {
            case 0:
                SelectCountries(currentPlayer);
                break;

            case 1:
                RollingDice();
                break;
        }
    }

    // Author: Bradley & Harvey
    private void SelectCountries(Player currentPlayer)
    {
        if (cameraScript.selectedCountry == -99) return; // FUCKING WORKS!

        bool isOwned = currentPlayer.getOwnedRegions().Contains(cameraScript.selectedCountryTag);

        if (isOwned)
        {
            attackingCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();
            foreach (var countryTag in attackingCountry.getAdjacentRegions())
            {
                if (!currentPlayer.getOwnedRegions().Contains(countryTag))
                    GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
            }
        }
        else
        {
            defendingCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();
            foreach (var countryTag in defendingCountry.getAdjacentRegions())
            {
                if (currentPlayer.getOwnedRegions().Contains(countryTag))
                    GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
            }

        }

    }

    // Author: 
    private void RollingDice()
    {

    }
}