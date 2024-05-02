using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortifyPhase : MonoBehaviour
{
    [Header("Selected Countries")]
    [SerializeField] RegionV2 fromCountry;
    [SerializeField] RegionV2 fortifiedCountry;

    [Header("Game Objects")]
    [SerializeField] OrbitalCamera cameraScript;
    [SerializeField] GameObject troopsSlider;
    private SliderController troopsSliderScript =>
        troopsSlider.GetComponent<SliderController>();
    [SerializeField] GameObject confirmButton;

    [Header("Fortifying Troops")]
    [SerializeField] int troopsToMove = 0;

    private bool isFortifying = false;

    void Start()
    {
        // Initialize any necessary components or variables
    }

    void Update()
    {
        // Empty Update method
    }

    public void PhaseLoop(Player currentPlayer)
    {
        if (!isFortifying)
        {
            SelectCountries(currentPlayer);
        }
    }

    private void SelectCountries(Player currentPlayer)
    {
        if (fromCountry == null)
            currentPlayer.GetOwnedRegions().ForEach(r => GameObject.FindGameObjectWithTag(r).GetComponent<RegionV2>().isHighlighted = true);

        if (cameraScript.selectedCountry < 0)
        { 
            fromCountry = null;
            return;
        }

        bool isOwned = currentPlayer.isOwnedRegion(cameraScript.selectedCountryTag);

        if (isOwned)
        {
            if (fromCountry == null || !fromCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
            {
                fromCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

                foreach (var countryTag in fromCountry.getAdjacentRegions())
                {
                    if (currentPlayer.isOwnedRegion(countryTag))
                        GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
                }
            }
            else if (fortifiedCountry == null || fromCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
            {
                fortifiedCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

                if (!fromCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
                {
                    fortifiedCountry = null;
                }
                else if (fromCountry == fortifiedCountry)
                {
                    fromCountry = null;
                    fortifiedCountry = null;
                }
            }
        }
        else cameraScript.selectedCountry = -99;

        if (fromCountry != null && fortifiedCountry != null)
        {
            isFortifying = true;
            SetupFortifyUI();
        }
    }

    private void SetupFortifyUI()
    {
        cameraScript.isClickLocked = true;
        troopsSlider.SetActive(true);
        confirmButton.SetActive(true);

        troopsSliderScript.maxValue = fromCountry.getTroopNum() - 1;
        troopsSliderScript.minValue = 0;
        troopsSliderScript.value = 1;
    }

    public void FortifyTroops()
    {
        //if (troopsSlider.activeSelf)
        //{
        //    int sliderValue = (int)troopsSliderScript.value;
        //    troopsUIText.GetComponent<Text>().text = sliderValue.ToString();
        //}

        //if (Input.GetButtonDown("Confirm"))
        //{
        troopsToMove = (int)troopsSliderScript.value;
        fromCountry.MoveTroops(fortifiedCountry, troopsToMove);

        ResetFortifyPhase();
        //}
    }

    private void ResetFortifyPhase()
    {
        isFortifying = false;
        fromCountry = null;
        fortifiedCountry = null;
        troopsToMove = 0;
        cameraScript.selectedCountry = -99;

        cameraScript.isClickLocked = false;
        troopsSlider.SetActive(false);
        confirmButton.SetActive(false);
    }
}
