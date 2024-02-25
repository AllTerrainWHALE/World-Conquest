using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCamera : MonoBehaviour
{
    // Start is called before the first frame update

    //Created by Harvey Apivor
    //This script is used to handle the camera movement
    //It is used to move the camera to the region that is clicked
    //It is attached to the camera game object
    //Used to handle camera orbiting and zooming
    public Vector3 targetPosition = Vector3.zero;
    [SerializeField]  float distance = 100.0f;
    [SerializeField]  float distanceMax = 150.0f;
    [SerializeField]  float distanceMin = 40.0f;
    [SerializeField] float zoomSpeed = 2f;
    [SerializeField] float mouseSpeed = 3;
    [SerializeField] float orbitDamping = 10;
    public Vector3 actualPosition = Vector3.zero;
    public int selectedCountry = -99;
    public Vector3 localRotation;
    void Start()
    { 
        //Initialise the camera position
        Quaternion QT = Quaternion.Euler(transform.rotation.y, transform.rotation.x, 0f);
        localRotation.x = Input.GetAxis("Mouse X") + transform.rotation.x;
        localRotation.y = Input.GetAxis("Mouse Y") + transform.rotation.y;
    }
    // Update is called once per frame
    void Update()
    {
        //Handle the camera orbiting and zooming
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) {
            distance = Mathf.Clamp(distance - zoomSpeed, distanceMin, distanceMax);
            //distance -= zoomSpeed;
        }
        else if (scroll < 0f) {
             distance = Mathf.Clamp(distance + zoomSpeed, distanceMin, distanceMax);
            //distance += zoomSpeed;
        }
       
        //Clamp the distance
        
   
        //Handle the camera orbiting
        if (Input.GetMouseButton(0) || (scroll != 0f)){
            //Get the mouse input
            localRotation.x += ((Input.GetAxis("Mouse X") * mouseSpeed));
            localRotation.y -= ((Input.GetAxis("Mouse Y") * mouseSpeed));
            //Clamp the y rotation
            localRotation.y = Mathf.Clamp(localRotation.y, 0f, 80f);
            
            //Set the camera position
        }
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0f);
            //Lerp the camera rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, QT, Time.deltaTime * orbitDamping );
        Vector3 rotation = transform.rotation * new Vector3(0.0f, 0.0f, -distance) + actualPosition;
        //Lerp the camera position
        Vector3 rotpluspos = Vector3.Lerp(transform.position, rotation,  Time.deltaTime * orbitDamping);
        transform.position =  rotpluspos;
        //Set the target position
        if (actualPosition != targetPosition){
            actualPosition = Vector3.Lerp(actualPosition, targetPosition, Time.deltaTime * orbitDamping);
        }
        
    }

}
