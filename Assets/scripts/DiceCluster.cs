using System.Collections;
using System.Collections.Generic;
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

    public void SpawnDice()
    {
        foreach (GameObject die in dice) Destroy(die);

        for (int i = 0; i < 5; i++)
        {
            dice.Add(Instantiate(diePrefab,
                new Vector3(
                    transform.position.x,// + Random.Range(-50, 50),
                    transform.position.y,
                    transform.position.z// + Random.Range(-50, 50)
                ),
                Quaternion.Euler(
                    Random.Range(0, 360),
                    Random.Range(0, 360),
                    Random.Range(0, 360)
                    )
            ));
        }
    }
}
