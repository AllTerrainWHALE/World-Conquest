using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DieBehaviour : MonoBehaviour
{
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

    public int GetRoll()
    {
        List<float> orientations = new()
        {
            Vector3.Dot(-transform.right, Vector3.up),      // 1
            Vector3.Dot(-transform.up, Vector3.up),         // 2
            Vector3.Dot(transform.right, Vector3.up),       // 3
            Vector3.Dot(transform.up, Vector3.up),          // 4
            Vector3.Dot(-transform.forward, Vector3.up),    // 5
            Vector3.Dot(transform.forward, Vector3.up),     // 6
        };

        rollVal = orientations.IndexOf(orientations.OrderBy(item => Math.Abs(1.0 - item)).First()) + 1;
        return rollVal;
    }

    public bool IsSettled() => rb.velocity.magnitude <= 0.01f;

}
