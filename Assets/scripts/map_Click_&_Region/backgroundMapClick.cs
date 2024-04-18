using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMapClick : MonoBehaviour
{
    // Start is called before the first frame update
    public OrbitalCamera cameraController;
    public int countryID;


    
    void Start()
    {
       
    }
    // Update is called once per frame
    private float lastClickTime = 0f;
    private const float clickDelay = 0.5f;
    private int clickCount = 0;

    void Update()
    {
        if (cameraController == null || cameraController.isClickLocked) return;

        //Handle the click event on the regions
        if (Input.GetMouseButtonDown(0))
        {
            //When the region is clicked, move the camera to the region
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Check if the region is clicked
            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                //Check if the click is a triple click
                if (Time.time - lastClickTime < clickDelay)
                {
                    clickCount++;
                }
                else
                {
                    clickCount = 1;
                }

                lastClickTime = Time.time;
                //If the click is a triple click, move the camera to the region
                if (clickCount == 3)
                {
                    cameraController.targetPosition = transform.position;
                    cameraController.selectedCountry = -99;
                    clickCount = 0;
                }
            }
        }
    }


   
    
}
