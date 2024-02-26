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
    //objectTargetY is used to lerp the object to the target position
    private Vector3 objectTargetY= new Vector3(0,0,0);

    private void OnMouseUp(){
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Check if the region is clicked
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            if (cameraController != null){
                cameraController.targetPosition = transform.position;
                //Set the selected country
                cameraController.selectedCountry = countryID;
            }
        }
    }
    void Start()
    {
        //Initialise the object positions
        objectOriginalY = transform.position;
        //Raise the object
        objectRaisedY = objectOriginalY + new Vector3(0, yRaise, 0);
        // Set the target position to the original position
        objectTargetY = objectOriginalY;
    }

    // Update is called once per frame
    void Update()
    {
        //Lerp the object to the target position
        if (cameraController != null){
            //Check if the country is selected
            if (countryID == cameraController.selectedCountry) {
                isSelected = true;
                //Raise the object
                objectTargetY = objectRaisedY;
            }
            else {
                //Lower the object
                isSelected = false;
                objectTargetY = objectOriginalY;
            }
            //Lerp the object to the target position
            if (isSelected){
                float timeModifier = 2.5f;
                float heightModifier = 0.15f;
                //Move the object up and down
                objectTargetY.y += Mathf.Sin(Time.time * timeModifier ) * heightModifier;
            }
            //Raise the object if the country is selected
            if (countryID >= 0)
                transform.position = Vector3.Lerp(transform.position, objectTargetY, Time.deltaTime * 5);
        }
    }
}
