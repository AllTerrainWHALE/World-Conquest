using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_Dice_Rotator : MonoBehaviour {
    
    [SerializeField]float Yspeed;
    [SerializeField] float Xspeed;

    void Update() {
        transform.Rotate(Vector3.up * Yspeed * Time.deltaTime); // Rotate around Y-axis
        transform.Rotate(Vector3.right * Xspeed * Time.deltaTime); // Rotate around Y-axis
    }

}
