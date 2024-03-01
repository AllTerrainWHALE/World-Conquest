<<<<<<< Updated upstream
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
=======
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Author: Bradley Hopper
//
// Enum file to store the values for artillery, cavalry and infantry pieces
//
class Troop {

}

enum TroopTypes {
    Artillery = 10,
    Cavalry = 5,
    Infantry = 1
}
>>>>>>> Stashed changes
