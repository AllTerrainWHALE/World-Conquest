using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using UnityEditor.Rendering;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

// Authors: Harvey Apivor and Eoin Howard Scully
// 
// This script is a combination of Region.cs and selectCountry.cs
//
// This Object represents one region on the game board, it handles the following:
//   - Handling a region selection
//   - Adding a troop to the region
//   - Removing a troop from the region
//   - Adjacency

public class RegionV2 : MonoBehaviour
{

    [Header("Camera")]
    [SerializeField] OrbitalCamera cameraController;
    [SerializeField] Camera cam;

    [Header("Region Selection")]
    [SerializeField] int countryID; //obsoleted by countryTag
    // id of the continent this region belongs to
    [SerializeField] string continentID;
    [SerializeField] String countryTag; //tag of region
    [SerializeField] bool isSelected = false;
    [SerializeField] float yRaise = 1.0f;
    [SerializeField] Vector3 objectOriginalY = new Vector3(0,0,0);
    [SerializeField] Vector3 objectRaisedY= new Vector3(0,0,0);
    //objectTargetY is used to lerp the object to the target position
    [SerializeField] Vector3 objectTargetY= new Vector3(0,0,0);

    [Header("Adjacent Region(s) To Selected Regions")]
    // tags of all regions this region is adjacent to
    [SerializeField] List<string> adjacentRegions;

    [Header("Troops")]
    // the number of troops inside the region
    [SerializeField] int numberOfTroops = 0;
    // prefabs of board tokens
    [SerializeField] GameObject infantryModel;
    [SerializeField] GameObject calvaryModel;
    [SerializeField] GameObject artilleryModel;
    // lists containing game objects to represent board tokens
    [SerializeField] List<GameObject> infantryList = new List<GameObject>();
    [SerializeField] List<GameObject> calvaryList = new List<GameObject>();
    [SerializeField] List<GameObject> artilleryList = new List<GameObject>();

    [Header("WIP")]
    //center and radius of largest circle within the region (WIP)
    [SerializeField] float radius;
    [SerializeField] Vector3 center;


    private void OnMouseUp(){
        RaycastHit hit;
        Debug.Log("Mouse Clicked");
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Check if the region is clicked
        if (Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
             Debug.Log(transform.tag);
            if (cameraController != null){
                cameraController.targetPosition = transform.position;
                //Set the selected country
                cameraController.selectedCountry = countryID;
                cameraController.selectedCountryTag = countryTag;
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
        // Finds center
        // center = findCenter();
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

    // Author: Eoin Howard Scully
    //
    // Method that flips the 'selected' attribute
    //
    // In future prototypes this will also interact 
    // with the 'meta' object to keep a record of selected objects
    public void selectRegion() 
    {
        //flips selected
        isSelected = !isSelected;
    }

    // Author: Eoin Howard Scully
    //
    // Method that adds a number of troops on top of an object
    //
    public void addTroop(int troopNum = 1)
    {

        // Calculates the ideal number of each token that should be on the board

        int infantryVal = (int)TroopTypes.Infantry;
        int calvaryVal = (int)TroopTypes.Cavalry;
        int artilleryVal = (int)TroopTypes.Artillery;

        int totalTroops = troopNum + numberOfTroops;

        int artilleryCount = totalTroops / artilleryVal;
        int calvaryCount = (totalTroops % artilleryVal) / calvaryVal;
        int infantryCount = ((totalTroops % artilleryVal) % calvaryVal) / infantryVal;

        while(artilleryList.Count != artilleryCount)
        {
            // Defines a circle and picks a random point inside it to place the troop
            artilleryList.Add(placeInCircle(artilleryModel));

        }

        while(calvaryList.Count != calvaryCount)
        {
            if(calvaryList.Count < calvaryCount) {
                // Adds one calvary until the amount of the board matches the calculated amount
                calvaryList.Add(placeInCircle(calvaryModel));

            } else {
                // Removes one calvary until the amount on the board matches the calculated amount
                Destroy(calvaryList[0]);
                calvaryList.Remove(calvaryList[0]);

            }
        }

        while(infantryList.Count != infantryCount)
        {
            if(infantryList.Count < infantryCount) {
                // Adds one infantry until the amount of the board matches the calculated amount
                infantryList.Add(placeInCircle(infantryModel));

            } else {
                // Removes one infantry until the amount on the board matches the calculated amount
                Destroy(infantryList[0]);
                infantryList.Remove(infantryList[0]);

            }
        }

        numberOfTroops += troopNum;
    }

    // Author: Eoin Howard Scully
    //
    // This method removes a number of troops from the board
    // Returns false if the reduction results in negative troops
    // 
    bool removeTroop(int troopNum = 1)
    {
        // Logs an error if the user tries to remove too many troops
        if ((numberOfTroops - troopNum) < 0) {
            Debug.LogError("You are trying to remove more troops than exist in this region");
            return false;
        }
        
        // Calculates the ideal number of each token that should be on the board
        
        int infantryVal = (int)TroopTypes.Infantry;
        int calvaryVal = (int)TroopTypes.Cavalry;
        int artilleryVal = (int)TroopTypes.Artillery;

        int totalTroops = numberOfTroops - troopNum;

        int artilleryCount = totalTroops / artilleryVal;
        int calvaryCount = (totalTroops % artilleryVal) / calvaryVal;
        int infantryCount = ((totalTroops % artilleryVal) % calvaryVal) / infantryVal;

        while(artilleryList.Count != artilleryCount)
        {
            // Defines a circle and picks a random point inside it to place the troop
            if(artilleryList.Count < artilleryCount) {
                artilleryList.Add(placeInCircle(artilleryModel));
            } else {
                Destroy(artilleryList[0]);
                artilleryList.Remove(artilleryList[0]);
            }
        }

        while(calvaryList.Count != calvaryCount)
        {
            if(calvaryList.Count < calvaryCount) {
                // Adds one calvary until the amount of the board matches the calculated amount
                
                calvaryList.Add(placeInCircle(calvaryModel));
            } else {
                // Removes one calvary until the amount on the board matches the calculated amount

                Destroy(calvaryList[0]);
                calvaryList.Remove(calvaryList[0]);
            }
        }

        while(infantryList.Count != infantryCount)
        {
            if(infantryList.Count < infantryCount) {
                // Adds one calvary until the amount of the board matches the calculated amount
                infantryList.Add(placeInCircle(infantryModel));
            } else {
                // Removes one calvary until the amount on the board matches the calculated amount
                Debug.Log(infantryList.Count);
                Destroy(infantryList[0]);
                infantryList.Remove(infantryList[0]);
            }
        }

        numberOfTroops -= troopNum;

        return true;
    }

    // Author: Eoin Howard Scully
    //
    // Finds the center and radius of the largest possible circle
    // This circle is then used when placing tokens
    // (WIP)
    /*
    private Vector3 findCenter() {

        Vector3 center = new Vector3();

        Vector3[] vertexes = GetComponent<MeshFilter>().mesh.vertices;

        List<float> Xs = new List<float>();
        List<float> Zs = new List<float>();

        int i = 0;
        do {
            try {
                Xs.Add(vertexes[i].x);
                Zs.Add(vertexes[i].z);
                i++;
            } catch {
                break;
            }
        } while(true);

        radius = Math.Max(Xs.Max() - Xs.Min(), Zs.Max() - Zs.Min()) / 2;

        float centerX = (Xs.Min() + radius);
        float centerZ = (Zs.Min() + radius);

        center = new Vector3(centerX, objectRaisedY.y, centerZ);

        center += transform.position;

        return center;
    }
    */

    // Author: Eoin Howard Scully
    // WIP
    //
    // This method spawns generates random points in a rectangle around a region
    // until it finds a point inside the region
    // It then spawns an object at that point

    /*
    private void spawnInBounds(GameObject objectToPlace, Bounds bounds_)
    {

        for(int i = 0; i < Bounds.Count; i++)
        {

        }
        
        float randX = UnityEngine.Random.Range(Xs.Max(),Xs.Min());
        float randZ = UnityEngine.Random.Range(Zs.Max(),Zs.Min());

        float newY = transform.position.y;

        Vector3 candidatePosistion = new Vector3(randX, newY, randZ);

        if(Bounds.contains(candidatePosistion))
        {
            GameObject newToken = Instantiate(objectToPlace, candidatePosistion, transform.rotation);
        }
        else
        {
            spawnInBounds(objectToPlace);
        }

    }
    */

    // Author: Eoin Howard Scully
    //
    // This method places one object in a circlular area above the current object
    // It exits to reduce duplicate code
    //
    private GameObject placeInCircle(GameObject objectToPlace) {

        // Defines a circle and picks a random point inside it to place the troop

        float theta = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; // Random Degree in Radians

        radius = 5;

        float randX = UnityEngine.Random.Range(0 , (radius * Mathf.Cos(theta)));
        float randZ = UnityEngine.Random.Range(0 , (radius * Mathf.Sin(theta)));
        float newY = 1.0f;

        var dist = new Vector3(randX, newY, randZ);

        GameObject newToken = Instantiate(objectToPlace, transform.position + dist, transform.rotation);

        //newToken.transform.parent = transform;

        return newToken;
    }

    public int getTroopNum()
    {
        return numberOfTroops;
    }

    // Author: Eoin Howard Scully
    //
    // Simple getter method
    // Returns a lists of tags for all the adjacent regions as strings
    //
    public List<string> getAdjacentRegions()
    {
        return adjacentRegions;
    }

    // Author: Eoin Howard Scully
    //
    // 'Moves' a certain number of troops from this region to another region
    // Moving is simulated by removing troops here and making new ones in the target region
    //
    public void MoveTroops(RegionV2 targetRegion, int numOfTroops) 
    {
        if (numOfTroops <= numberOfTroops) {
            removeTroop(numOfTroops);
            targetRegion.addTroop(numOfTroops);
        }
    }
}

