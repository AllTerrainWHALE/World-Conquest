using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DieBehaviour : MonoBehaviour
{
    /*
     * Made by Bradley Hopper
     * 
     * This script is made to handle the behaviour of each die.
     * It holds methods to retieve the value rolled from the die,
     *  and to check whether it is currently moving or not
     */

    public Rigidbody rb;
    public int rollVal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calculates which face of the die is pointing upwards, and returns that faces corresponding value
    /// </summary>
    /// <returns>
    /// A single integer of the dice roll value
    /// </returns>
    public int GetRoll()
    {
        List<float> orientations = new()
        {
            Vector3.Dot(-transform.right, Vector3.up),      // 1
            Vector3.Dot(-transform.forward, Vector3.up),    // 2
            Vector3.Dot(transform.right, Vector3.up),       // 3
            Vector3.Dot(transform.up, Vector3.up),          // 4
            Vector3.Dot(transform.forward, Vector3.up),     // 5
            Vector3.Dot(-transform.up, Vector3.up),         // 6
        };

        rollVal = orientations.IndexOf(orientations.OrderBy(item => Math.Abs(1.0 - item)).First()) + 1;
        return rollVal;
    }

    /// <summary>
    /// Checks whether the die is currently moving, and is declared as "not moving" if the magnitude of the die is less than 0.05
    /// </summary>
    /// <returns>
    /// A boolean value declaring whether the die is settled or not
    /// </returns>
    public bool IsSettled() => rb.velocity.magnitude <= 0.05f && transform.position.y <= 10;

}
