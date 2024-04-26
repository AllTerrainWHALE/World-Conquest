using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public int cardsSetsTradedIn; // Ilja: It would be better to store it here

    void Start()
    {

    }

    void Update()
    {

    }

}