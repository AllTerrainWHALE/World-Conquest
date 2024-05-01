using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{

    [Header("Game Loop Controller")]
    // The current phase and turn number
    [SerializeField] int phaseNumber;
    [SerializeField] int turnNumber = 0;

    [Header("Phase Scripts")]
    // The phases have been split into induvidual classes
    // This is to help support paralell development
    // The turn order is:
    //    Setup --> Deployment --> Attack --> Fortify
    //    (note that setup occurs once at the beginning of the game)
    [SerializeField] SetupPhase Setup;
    [SerializeField] DeploymentPhase Deployment;
    [SerializeField] AttackPhase Attack;
    [SerializeField] FortifyPhase Fortify;

    [Header("Main Deck Object")]
    [SerializeField] GameObject playerDeckContainer;

    [Header("Phase UI Objects")]
    [SerializeField] TMP_Text uiPhaseText;
    [SerializeField] GameObject uiPhaseOneIndicator;
    [SerializeField] GameObject uiPhaseTwoIndicator;
    [SerializeField] GameObject uiPhaseThreeIndicator;
    [SerializeField] GameObject uiNextPhaseButton;

    [Header("Players")]
    [SerializeField] GameObject playerGO;
    [SerializeField] List<Player> playerList;

    [Header("Continents")]
    [SerializeField] List<Continent> continents;

    void Start()
    {
        Setup = GameObject.Find("setup_Phase").GetComponent<SetupPhase>();
        Deployment = GameObject.Find("deployment_Phase").GetComponent<DeploymentPhase>();
        Attack = GameObject.Find("attack_Phase").GetComponent<AttackPhase>();
        Fortify = GameObject.Find("fortify_Phase").GetComponent<FortifyPhase>();

        uiPhaseText = GameObject.Find("phase_Text").GetComponent<TMP_Text>();
        uiPhaseOneIndicator = GameObject.Find("active_Bars/phase_1");
        uiPhaseTwoIndicator = GameObject.Find("active_Bars/phase_2");
        uiPhaseThreeIndicator = GameObject.Find("active_Bars/phase_3");
        uiNextPhaseButton = GameObject.Find("end_Phase_Button/end_Phase_Button (1)");

        uiPhaseOneIndicator.active = false;
        uiPhaseTwoIndicator.active = false;
        uiPhaseThreeIndicator.active = false;

        /*
        System.Random rnd = new System.Random();
        List<string> selectedPlayerNames = Player.defaultPlayerNames
            .OrderBy(x => rnd.Next())
            .Take(PlayerPrefs.GetInt("PlayerCount"))
            .ToList();

        GameObject obj;
        Player script;
        for (int i = 0; i < selectedPlayerNames.Count; i++)
        {
            obj = Instantiate(playerGO);
            script = obj.GetComponent<Player>();

            obj.name = selectedPlayerNames[i];
            script.Init(
                rnd.Next(100),
                (TroopColour)Enum.Parse(
                    typeof(TroopColour),
                    Enum.GetName(typeof(TroopColour),i)
                    )
                );

            playerList.Add(script);
        }
        */

        phaseNumber = 0;

        Setup.Init(playerList);
    }

    void Update()
    {
        switch (phaseNumber)
        {
            case 0: // Setup phase processes
                Setup.PhaseLoop();
                break;

            case 1: // Deployment phase processes
                Deployment.PhaseLoop(playerList[turnNumber]);
                break;

            case 2: // Attack phase processes
                Attack.PhaseLoop(playerList[turnNumber]);
                break;

            case 3: // Fortify phase processes
                Fortify.PhaseLoop(playerList[turnNumber]);
                break;

            case 10: // Finished game phase processes
                break;

            default:
                // Increment to the next player, and skip those who no longer have owned countries
                turnNumber = (turnNumber + 1) % playerList.Count();
                while (playerList[turnNumber].GetOwnedRegions().Count == 0)
                    turnNumber = (turnNumber + 1) % playerList.Count();

                if (playerList.Count(p => p.GetOwnedRegions().Count != 0) > 1)
                    incrementPhase();
                else
                {
                    phaseNumber = 10;
                    uiNextPhaseButton.GetComponent<Button>().interactable = false;
                }
                break;
        }
    }

    public void incrementPhase()
    {
        phaseNumber = (phaseNumber % 4) + 1;

        // Handling inter-phase processes
        switch (phaseNumber)
        {
            case 1:
                uiPhaseText.text = "DEPLOYMENT";
                uiPhaseOneIndicator.active = true;

                playerList[turnNumber].SetBonus(
                    continents.Where(c => c.PlayerRulesContinent(playerList[turnNumber]))
                    .Select(c => c.bonus)
                    .Sum()
                    );

                playerList[turnNumber].DisplayCards();

                break;

            case 2:
                uiPhaseText.text = "ATTACK";
                uiPhaseTwoIndicator.active = true;
                uiPhaseOneIndicator.active = false;

                playerList[turnNumber].HideCards();

                break;

            case 3:
                uiPhaseText.text = "FORTIFY";
                uiPhaseThreeIndicator.active = true;
                uiPhaseTwoIndicator.active = false;

                break;

            case 4:
                uiPhaseThreeIndicator.active = false;

                break;

            case 10:
                break;

        }

    }

    public void SetPlayerList(List<Player> newPlayers) => playerList = newPlayers;

    public List<Player> GetPlayerList() => playerList;

}