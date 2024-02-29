using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : MonoBehaviour
{
    public TroopTypes troopType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

enum TroopTypes
{
    Infantry = 1,
    Cavalry = 5,
    Artillery = 10
}
