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
        else
        {
            FortifyTroops();
        }
    }

    private void SelectCountries(Player currentPlayer)
    {
        if (cameraScript.selectedCountry < 0) return;

        bool isOwned = currentPlayer.isOwnedRegion(cameraScript.selectedCountryTag);

        if (isOwned)
        {
            if (fromCountry == null)
            {
                fromCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

                foreach (var countryTag in fromCountry.getAdjacentRegions())
                {
                    if (currentPlayer.isOwnedRegion(countryTag))
                        GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
                }
            }
            else if (fortifiedCountry == null)
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

        if (fromCountry != null && fortifiedCountry != null)
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

    private void ResetFortifyPhase()
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
