using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AttackPhase : MonoBehaviour
{
    [Header("Selected Countries")]
    [SerializeField] RegionV2 attackingCountry;
    [SerializeField] RegionV2 defendingCountry;

    [Header("")]
    // 0 = country selection | 1 = dice rolling
    [SerializeField] int subPhaseNumber = 0;

    [Header("Game Objects")]
    [SerializeField] OrbitalCamera cameraScript;
    [SerializeField] DiceCluster diceCluster;
    [SerializeField] GameObject rollDiceButton;
    [SerializeField] Button nextPhaseButton;

    [SerializeField] GameObject troopsSlider;
    [SerializeField] SliderController troopsSliderScript =>
        troopsSlider.GetComponent<SliderController>();

    [Header("Battling Troops")]
    [SerializeField] Player currentPlayer;
    [SerializeField] int attackingTroops = 0;
    [SerializeField] int defendingTroops = 0;

    [Header("")]
    [SerializeField] bool isRolling = false;


    // Start is called before the first frame update
    void Start()
    {
        // cameraScript = GetComponent<Camera>().GetComponent<OrbitalCamera>();
        // troopsSliderScript = troopsSlider.GetComponent<SliderController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Author: 
    public void PhaseLoop(Player _currentPlayer)
    {
        currentPlayer = _currentPlayer;

        switch (subPhaseNumber)
        {
            case 0: // Attacking and Defending country selection
                SelectCountries();
                break;

            case 1: // Inter-phase processes

                cameraScript.isClickLocked = true;
                cameraScript.selectedCountry = defendingCountry.countryID;
                cameraScript.distance = cameraScript.distanceMin;

                diceCluster.spawnOrigin.x = defendingCountry.transform.position.x;
                diceCluster.spawnOrigin.z = defendingCountry.transform.position.z;

                troopsSlider.SetActive(true);
                rollDiceButton.GetComponent<Button>().interactable = true;

                nextPhaseButton.interactable = false;

                troopsSliderScript.maxValue = attackingCountry.getTroopNum() - 1;
                defendingTroops = defendingCountry.getTroopNum();

                subPhaseNumber += 1;
                break;

            case 2: // Rolling the dice
                cameraScript.targetPosition = defendingCountry.transform.position;
                
                Attacking();
                break;

            case 3: // Reset restrictions and loop back to selecting countries
                cameraScript.isClickLocked = false;
                cameraScript.selectedCountry = -99;
                cameraScript.distance = 100f;

                rollDiceButton.GetComponent<Button>().interactable = false;

                nextPhaseButton.interactable = true;

                attackingCountry = null;
                defendingCountry = null;

                subPhaseNumber = 0;
                break;
        }
    }

    // Author: Bradley & Harvey
    private void SelectCountries()
    {
        if (cameraScript.selectedCountry < 0) return;

        bool isOwned = currentPlayer.isOwnedRegion(cameraScript.selectedCountryTag);

        if (isOwned)
        {
            attackingCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

            if (defendingCountry != null && !defendingCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
                defendingCountry = null;

            foreach (var countryTag in attackingCountry.getAdjacentRegions())
            {
                if (!currentPlayer.isOwnedRegion(countryTag))
                    GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
            }
        }
        else
        {
            defendingCountry = GameObject.FindGameObjectWithTag(cameraScript.selectedCountryTag).GetComponent<RegionV2>();

            if (attackingCountry != null && !attackingCountry.isAdjacentRegion(cameraScript.selectedCountryTag))
                attackingCountry = null;

            foreach (var countryTag in defendingCountry.getAdjacentRegions())
            {
                if (currentPlayer.isOwnedRegion(countryTag))
                    GameObject.FindGameObjectWithTag(countryTag).GetComponent<RegionV2>().isHighlighted = true;
            }

        }

        if (attackingCountry != null &&
            defendingCountry != null &&
            attackingCountry != defendingCountry
            ) subPhaseNumber += 1;
    }

    // Author: Bradley & Harvey
    private void Attacking()
    {
        //if (troopsSliderUIGroup.active)
        //{
        //    int sliderVal = (int) troopsUISlider.GetComponent<Slider>().value;
        //    troopsUIText.GetComponent<TMP_Text>().SetText(sliderVal.ToString());
        //}

        if (diceCluster.AllDiceSettled() && isRolling) // && diceCluster.GetRollTotal() != (0,0))
        {
            IEnumerable<int> attackerRolls = diceCluster.GetAttackerRolls();
            IEnumerable<int> defenderRolls = diceCluster.GetDefenderRolls();

            for (int i=0; i < Mathf.Min(attackerRolls.Count(),defenderRolls.Count()); i++)
            {
                if (attackerRolls.ElementAt(i) > defenderRolls.ElementAt(i))
                {
                    defendingCountry.removeTroop();
                    defendingTroops = Mathf.Max(defendingTroops - 1, 0);
                } else
                {
                    if (attackingTroops != 0) attackingCountry.removeTroop();
                    attackingTroops = Mathf.Max(attackingTroops - 1, 0);
                }
            }

            isRolling = false;
            rollDiceButton.GetComponent<Button>().interactable = true;
        }

        if ((attackingTroops <= 0 || defendingCountry.getTroopNum() <= 0) && !troopsSlider.active)
        {
            if (attackingTroops > 0)
            {
                defendingCountry.SetRulingPlayer(currentPlayer);
                attackingCountry.MoveTroops(defendingCountry, attackingTroops);
            }

            subPhaseNumber += 1;
        }

    }

    public void RollDice()
    {
        if (troopsSlider.active)
        {
            attackingTroops = (int) troopsSliderScript.value;
            troopsSlider.SetActive(false);
        }

        if (diceCluster.AllDiceSettled())
        {
            diceCluster.SpawnDice(Mathf.Min(attackingTroops, 3), Mathf.Min(defendingTroops, 2));
            rollDiceButton.GetComponent<Button>().interactable = false;
            isRolling = true;
        }
    }
}