using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnDie()
    {
        Instantiate(this,
            new Vector3(
                transform.position.x + Random.Range(-20, 20),
                transform.position.y,
                transform.position.z + Random.Range(-20, 20)
            ),
            Quaternion.Euler(
                Random.Range(0, 360),
                Random.Range(0, 360),
                Random.Range(0, 360)
                )
        );
    }

}
