using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Created by Harvey Apivor
//This script is used to handle the click event on the regions
//It is used to move the camera to the region that is clicked
//It is attached to the region game object
public class selectCountry : MonoBehaviour
{
    // Start is called before the first frame update
    public OrbitalCamera cameraController;
    public int countryID;
    public bool isSelected = false;
    private float yRaise = 1.0f;

    private Vector3 objectOriginalY = new Vector3(0,0,0);
    private Vector3 objectRaisedY= new Vector3(0,0,0);

    private Vector3 objectTargetY= new Vector3(0,0,0);
    private void OnMouseDown()
    {
        
        if (cameraController != null) {
            cameraController.targetPosition = transform.position;
            cameraController.selectedCountry = countryID;
        }
        
    }
    void Start()
    {
        objectOriginalY = transform.position;
        objectRaisedY = objectOriginalY + new Vector3(0, yRaise, 0);
        objectTargetY = objectOriginalY;
    }

    // Update is called once per frame
    void Update()
    {

        if (cameraController != null){
            if (countryID == cameraController.selectedCountry) {
                isSelected = true;
                objectTargetY = objectRaisedY;
            }
            else {
                isSelected = false;
                objectTargetY = objectOriginalY;
            }
            if (isSelected){
                float timeModifier = 2.5f;
                float heightModifier = 0.15f;
                objectTargetY.y += Mathf.Sin(Time.time * timeModifier ) * heightModifier;
            }
            if (countryID >= 0)
                transform.position = Vector3.Lerp(transform.position, objectTargetY, Time.deltaTime * 5);
        }
    }
}
