using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{

    // The current phase and turn number
    [SerializeField] int phaseNumber;
    [SerializeField] int turnNumber = 0;

    [SerializeField] List<Player> playerList;
    [SerializeField] public int cardsSetsTradedIn; // Ilja: It would be better to store it here

    // The phases have been split into induvidual classes
    // This is to help support paralell development
    // The turn order is:
    //    Setup --> Deployment --> Attack --> Fortify
    //    (note that setup occurs once at the beginning of the game)
    [SerializeField] SetupPhase Setup;
    [SerializeField] DeploymentPhase Deployment;
    [SerializeField] AttackPhase Attack;
    [SerializeField] FortifyPhase Fortify;

    [SerializeField] TMP_Text uiPhaseText;
    [SerializeField] GameObject uiPhaseOneIndicator;
    [SerializeField] GameObject uiPhaseTwoIndicator;
    [SerializeField] GameObject uiPhaseThreeIndicator;

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

        uiPhaseOneIndicator.active = false;
        uiPhaseTwoIndicator.active = false;
        uiPhaseThreeIndicator.active = false;

        phaseNumber = 0;

        Setup.Init(playerList);
    }

    void Update()
    {
        switch (phaseNumber)
        {
            case 0:
                Setup.PhaseLoop();
                break;

            case 1:
                Deployment.PhaseLoop(playerList[turnNumber]);
                break;

            case 2:
                Attack.PhaseLoop(playerList[turnNumber]);
                break;

            case 3:
                Fortify.PhaseLoop(playerList[turnNumber]);
                break;

            default:
                turnNumber = (turnNumber + 1) % playerList.Count();
                incrementPhase();
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

                break;

            case 2:
                uiPhaseText.text = "ATTACK";
                uiPhaseTwoIndicator.active = true;
                uiPhaseOneIndicator.active = false;

                break;

            case 3:
                uiPhaseText.text = "FORTIFY";
                uiPhaseThreeIndicator.active = true;
                uiPhaseTwoIndicator.active = false;

                break;

            case 4:
                uiPhaseThreeIndicator.active = false;

                break;

        }

    }

    public void SetPlayerList(List<Player> newPlayers) => playerList = newPlayers;

    public List<Player> GetPlayerList() => playerList;

}