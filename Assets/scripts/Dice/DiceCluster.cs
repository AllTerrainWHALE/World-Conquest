using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceCluster : MonoBehaviour
{
    public GameObject diePrefab;
    private List<GameObject> dice = new();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDice(int diceCount)
    {
        DestroyDice();

        for (int i = 0; i < diceCount; i++)
        {
            GameObject die = Instantiate(diePrefab,
                new Vector3(
                    transform.position.x,
                    transform.position.y,
                    transform.position.z
                ),
                Quaternion.Euler(
                    Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)
                    )
            );

            dice.Add(die);
        }
    }

    public void DestroyDice()
    {
        dice.ForEach(d => Destroy(d));
        dice.Clear();
    }

    public int GetRollTotal() => dice.Sum(d => d.GetComponent<DieBehaviour>().GetRoll());

    public bool AllDiceSettled() => dice.Count(d => !d.GetComponent<DieBehaviour>().IsSettled()) == 0;

    public void TestingDiceyThings()
    {
        if (AllDiceSettled()) Debug.Log("Total Rolled: " + GetRollTotal());
        else Debug.Log("Dice not settled");
    }
}
