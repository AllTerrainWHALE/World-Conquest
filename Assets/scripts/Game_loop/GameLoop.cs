using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    // The current phase and turn number
    [SerializeField] int phaseNumber;
    [SerializeField] int turnNumber = 0;

    [SerializeField] List<Player> playerList;

    // The phases have been split into induvidual classes
    // This is to help support paralell development
    // The turn order is:
    //    Setup --> Deployment --> Attack --> Fortify
    //    (note that setup occurs once at the beginning of the game)
    [SerializeField] SetupPhase Setup = new SetupPhase(); 
    [SerializeField] DeploymentPhase Deployment = new DeploymentPhase();
    [SerializeField] AttackPhase Attack; // = new AttackPhase(); 
    [SerializeField] FortifyPhase Fortify = new FortifyPhase(); 

    void Start()
    { 

    }

    void Update()
    {
        switch (phaseNumber)
        {
            case 0:
                Setup.PhaseLoop();
                break;

            case 1:
                Deployment.PhaseLoop();
                break;

            case 2:
                Attack.PhaseLoop(playerList[turnNumber]);
                break;

            case 3:
                Fortify.PhaseLoop();
                break;

            default:
                phaseNumber = 0;
                break;
        }
    }

    public void SetPlayerList(List<Player> newPlayers)
    {
        playerList = newPlayers;
    }

    public List<Player> GetPlayerList()
    {
        return playerList;
    }

}