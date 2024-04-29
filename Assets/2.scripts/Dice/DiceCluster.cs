using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceCluster : MonoBehaviour
{
    /* Made by Bradley Hopper
     * 
     * This script is made to handle information collection
     *  of all the dice that are rolled onto the board.
     * It contains methods to roll a collection of dice,
     *  get the values of each attackers and defenders dice,
     *  and remove all the dice from the board
     */

    public GameObject attackerDie;
    private List<GameObject> attackerDice = new();

    public GameObject defenderDie;
    private List<GameObject> defenderDice = new();

    public Vector3 spawnOrigin = new Vector3(0, 60, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* Testing purposes only*/
    public void SpawnDice()
    {
        SpawnDice(3, 2);
    }
    /**/

    /// <summary>
    /// Spawns a collection of dice for the attacker and defender, using the passed in values.
    /// The attacker parameter is restricted to value bounds between 0 and 3 (exc., inc.)
    /// The defender parameter is restricted to value bounds between 0 and 2 (exc., inc.)
    /// </summary>
    /// <param name="attackerDiceCount">
    /// The number of attackers dice that should be spawned and rolled
    /// </param>
    /// <param name="defenderDiceCount">
    /// The number of defenders dice that should be spawned and rolled
    /// </param>
    public void SpawnDice(int attackerDiceCount, int defenderDiceCount)
    {
        // Ensure the provided dice rolls are within the bounds of
        // 1 to 3 for the attacker
        attackerDiceCount = Mathf.Max(1, Mathf.Min(attackerDiceCount, 3));
        // 1 to 2 for the defender
        defenderDiceCount = Mathf.Max(1, Mathf.Min(defenderDiceCount, 2));

        // Remove all current dice on the board
        /* Testing purposes only*/
        DestroyDice();
        

        // Spawn the attackers dice (red) in a random orientation
        // and add them to the list of attackers dice
        for (int i = 0; i < attackerDiceCount; i++)
        {
            attackerDice.Add(Instantiate(attackerDie,
                new Vector3(
                    spawnOrigin.x,
                    spawnOrigin.y,
                    spawnOrigin.z
                ),
                Quaternion.Euler(
                    Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)
                    )
            ));
        }

        // Spawn the defenders dice (white) in a random orientation
        // and add them to the list of defenders dice
        for (int i = 0; i < defenderDiceCount; i++)
        {
            defenderDice.Add(Instantiate(defenderDie,
                new Vector3(
                    spawnOrigin.x,
                    spawnOrigin.y,
                    spawnOrigin.z
                ),
                Quaternion.Euler(
                    Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)
                    )
            ));
        }
    }

    /// <summary>
    /// Clears the board of all dice.
    /// </summary>
    public void DestroyDice()
    {
        // Remove the dice game objects from the scene
        attackerDice.ForEach(d => Destroy(d));
        defenderDice.ForEach(d => Destroy(d));
        
        // Clear the dice lists, ready for the next roll
        attackerDice.Clear();
        defenderDice.Clear();
    }

    /// <summary>
    /// Get an integer tuple of the attackers and defenders dice roll total values
    /// </summary>
    /// <returns>
    /// An (int, int) tuple of the accumulative roll values for the attacker and defender, respectively.
    /// </returns>
    public (int,int) GetRollTotal() => (
        attackerDice.Sum(d => d.GetComponent<DieBehaviour>().GetRoll()),
        defenderDice.Sum(d => d.GetComponent<DieBehaviour>().GetRoll())
        );

    /// <summary>
    /// Retrieve a list of all the dice values that the attacker rolled, in order of largest to smallest
    /// </summary>
    /// <returns>
    /// An ordered integer list of all the rolled values for the attacker
    /// </returns>
    public IEnumerable<int> GetAttackerRolls() =>
        attackerDice.Select(d => d.GetComponent<DieBehaviour>().GetRoll()).OrderByDescending(d => d);

    /// <summary>
    /// Retrieve a list of all the dice values that the defender rolled, in order of largest to smallest
    /// </summary>
    /// <returns>
    /// An ordered integer list of all the rolled values for the defender
    /// </returns>
    public IEnumerable<int> GetDefenderRolls() =>
        defenderDice.Select(d => d.GetComponent<DieBehaviour>().GetRoll()).OrderByDescending(d => d);

    /// <summary>
    /// Checks whether all the dice on the board are still currently moving or are settled, and returns a boolean value to represent this
    /// </summary>
    /// <returns>
    /// A boolean value as to whether all dice are settled or not
    /// (true == settled, false == not settled)
    /// </returns>
    public bool AllDiceSettled() => attackerDice.Concat(defenderDice).Count(d => !d.GetComponent<DieBehaviour>().IsSettled()) == 0;

    /* Testing purposes only
    public void TestingDiceyThings()
    {
        if (AllDiceSettled())
        {
            Debug.Log(
                "Attacker Rolled: " + string.Join(", ", GetAttackerRolls()) +
                "\n" +
                "Defender Rolled: " + string.Join(", ", GetDefenderRolls())
            );
        }

        else Debug.Log("Dice not settled");
    }
    */
}
