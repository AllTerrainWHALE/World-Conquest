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
    [SerializeField] GameObject troopsSliderUIGroup;
    [SerializeField] GameObject troopsUISlider;
    [SerializeField] GameObject troopsUIText;
    [SerializeField] GameObject confirmButton;

    [Header("Fortifying Troops")]
    [SerializeField] int troopsToMove = 0; //amount of troops to forify

    private bool isFortifying = false;

    void Start()
    {
        // Initialize any necessary components or variables
    }

    void Update()
    {
        // Empty Update method
    }

    public void PhaseLoop(Player currentPlayer) //gets the current player object
    {
        if (!isFortifying)
        {
            SelectCountries(currentPlayer); //method to select countries
        }
        else
        {
            FortifyTroops(); //if done selecting countries fortify troops
        }
    }

    private void SelectCountries(Player currentPlayer)
    {
        if (cameraScript.selectedCountry < 0) return;

        bool isOwned = currentPlayer.isOwnedRegion(cameraScript.selectedCountryTag);

        if (isOwned)
        {
            fromCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

            if (fortifiedCountry != null && !fortifiedCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
                fortifiedCountry = null;

            foreach (var countryTag in fromCountry.getAdjacentRegions())
            {
                if (currentPlayer.isOwnedRegion(countryTag))
                    GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
            }
        }
        else
        {
            fortifiedCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

            if (fromCountry != null && !fromCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
                fromCountry = null;
        }

        if (fromCountry != null && fortifiedCountry != null && fromCountry != fortifiedCountry)
        {
            isFortifying = true;
            SetupFortifyUI();
        }
    }

    private void SetupFortifyUI()
    {
        cameraScript.isClickLocked = true;
        troopsSliderUIGroup.SetActive(true);
        confirmButton.SetActive(true);

        troopsUISlider.GetComponent<Slider>().maxValue = fromCountry.getTroopNum() - 1;
        troopsUISlider.GetComponent<Slider>().value = 1;
        troopsUIText.GetComponent<Text>().text = "1";
    }

    private void FortifyTroops()
    {
        if (troopsSliderUIGroup.activeSelf)
        {
            int sliderValue = (int)troopsUISlider.GetComponent<Slider>().value;
            troopsUIText.GetComponent<Text>().text = sliderValue.ToString();
        }

        if (Input.GetButtonDown("Confirm"))
        {
            troopsToMove = (int)troopsUISlider.GetComponent<Slider>().value;
            fromCountry.MoveTroops(fortifiedCountry, troopsToMove);

            ResetFortifyPhase();
        }
    }

    private void ResetFortifyPhase() //resets back to neutral
    {
        isFortifying = false;
        fromCountry = null;
        fortifiedCountry = null;
        troopsToMove = 0;

        cameraScript.isClickLocked = false;
        troopsSliderUIGroup.SetActive(false);
        confirmButton.SetActive(false);
    }
}