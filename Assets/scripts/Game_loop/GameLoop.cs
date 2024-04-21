using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    // The current phase and turn number
    [SerializeField] int phaseNumber;
    [SerializeField] int turnNumber;

    [SerializeField] List<Player> playerList = new List<Player>();

    // The phases have been split into induvidual classes
    // This is to help support paralell development
    // The turn order is:
    //    Setup --> Deployment --> Attack --> Fortify
    //    (note that setup occurs once at the beginning of the game)
    [SerializeField] SetupPhase Setup = new SetupPhase(); 
    [SerializeField] DeploymentPhase Deployment = new DeploymentPhase(); 
    [SerializeField] AttackPhase Attack = new AttackPhase(); 
    [SerializeField] FortifyPhase Fortify = new FortifyPhase(); 

    void Start()
    {
        for (int index = 1; index <= 4; index++)
        {
            Player newPlayer = new Player();
            newPlayer.Init(index);
            playerList.Add(newPlayer);
        }
        
        phaseNumber = 0;
        Setup.Init(playerList);
    }

    void Update()
    {
        switch (phaseNumber)
        {
            case 0:
                Debug.Log(Setup);
                Setup.PhaseLoop();
                break;
        }
    }

    public void incrementPhase() { phaseNumber += 1; }

    public void SetPlayerList(List<Player> newPlayers)
    {
        playerList = newPlayers;
    }

    public List<Player> GetPlayerList()
    {
        return playerList;
    }

}