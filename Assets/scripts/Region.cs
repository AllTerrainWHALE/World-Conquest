using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Region : MonoBehaviour
{

    // Created by: Eoin Howard Scully
<<<<<<< Updated upstream
    // This Object represents one region on the game board, it handles the following:
    //   - Selecting a region
    //   - Adding a troop to the region
    //   - Removing a troop from the region  
    //   - Adjacency
=======
    //
    // This Object represents one region on the game board, it handles the following:
    //   - Handling a region selection
    //   - Adding a troop to the region
    //   - Removing a troop from the region
    //   - Adjacency
    //
>>>>>>> Stashed changes
    public int numberOfTroops = 0;
    public List<string> adjacentRegions;
    public string continentID;
    public Boolean selected = false;
<<<<<<< Updated upstream
    public GameObject placeHolderCube;
    private List<GameObject> infantryList;
    private List<GameObject> calvaryList;
    private List<GameObject> artilleryList;
=======
    public GameObject infantryModel;
    public GameObject calvaryModel;
    public GameObject artilleryModel;
    public List<GameObject> infantryList = new List<GameObject>();
    public List<GameObject> calvaryList = new List<GameObject>();
    public List<GameObject> artilleryList = new List<GameObject>();
>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
        Debug.Log("Started!");
=======

>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {   
<<<<<<< Updated upstream
        //Detects if the region is selected, the 'P' key is selected and then adds a troop
        if(Input.GetKeyDown(KeyCode.P) & selected)
        {
            addTroop();
        }
        //Detects if the region is selected, the 'Q' key is pressed and then removes a troop
        if(Input.GetKeyDown(KeyCode.Q) & selected)
        {
            removeTroop();
        }
=======

>>>>>>> Stashed changes
    }

    //called on mouse click
    void OnMouseDown()
    {
<<<<<<< Updated upstream
        Debug.Log("Click detected!");
        selectRegion();
    }

    //Author: Eoin Howard Scully
    //
    //Method that flips the 'selected' attribute
    //
    //In future prototypes this will also interact 
    //with the 'meta' object to keep a record of selected objects
    void selectRegion() 
    {
        //flips selected
        Debug.Log("Selecting region!");
        selected = !selected;
    }

    //Author: Eoin Howard Scully
    //
    //Method that adds a 'troop' on top of an object
    //
    void addTroop(int troopNum)
    {
        
=======
        selectRegion();
    }

    // Author: Eoin Howard Scully
    //
    // Method that flips the 'selected' attribute
    //
    // In future prototypes this will also interact 
    // with the 'meta' object to keep a record of selected objects
    void selectRegion() 
    {
        //flips selected
        selected = !selected;
    }

    // Author: Eoin Howard Scully
    //
    // Method that adds a number of troops on top of an object
    //
    void addTroop(int troopNum = 1)
    {

        // Calculates the ideal number of each token that should be on the board

>>>>>>> Stashed changes
        int infantryVal = (int)TroopTypes.Infantry;
        int calvaryVal = (int)TroopTypes.Cavalry;
        int artilleryVal = (int)TroopTypes.Artillery;

        int totalTroops = troopNum + numberOfTroops;

        int artilleryCount = totalTroops / artilleryVal;
        int calvaryCount = (totalTroops % artilleryVal) / calvaryVal;
<<<<<<< Updated upstream
        int infantryCount = (totalTroops % artilleryVal) % calvaryVal;

        while(artilleryList.Count != artilleryCount)
        {
            //Defines a circle and picks a random point inside it to place the troop
            float radius = 2.0f;

            float theta = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; //Random Degree in Radians

            float randX = UnityEngine.Random.Range(0, (radius * Mathf.Cos(theta)) * transform.localScale.x);
            float randZ = UnityEngine.Random.Range(0, (radius * Mathf.Sin(theta)) * transform.localScale.z);
            float newY = 0.2f;

            var dist = new Vector3(randX, newY, randZ);

            artilleryList.Add(Instantiate(placeHolderCube, transform.position += dist, transform.rotation, transform));
=======
        int infantryCount = ((totalTroops % artilleryVal) % calvaryVal) / infantryVal;

        while(artilleryList.Count != artilleryCount)
        {
            // Defines a circle and picks a random point inside it to place the troop
            artilleryList.Add(placeInCircle(artilleryModel));

>>>>>>> Stashed changes
        }

        while(calvaryList.Count != calvaryCount)
        {
            if(calvaryList.Count < calvaryCount) {
<<<<<<< Updated upstream
                //Adds one calvary until the amount of the board matches the calculated amount

                //Defines a circle and picks a random point inside it to place the troop
                float radius = 2.0f;

                float theta = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; //Random Degree in Radians

                float randX = UnityEngine.Random.Range(0, (radius * Mathf.Cos(theta)) * transform.localScale.x);
                float randZ = UnityEngine.Random.Range(0, (radius * Mathf.Sin(theta)) * transform.localScale.z);
                float newY = 0.2f;

                var dist = new Vector3(randX, newY, randZ);

                calvaryList.Add(Instantiate(placeHolderCube, transform.position += dist, transform.rotation, transform));
            } else {
                //Removes one calvary until the amount on the board matches the calculated amount

                Destroy(calvaryList[0]);
=======
                // Adds one calvary until the amount of the board matches the calculated amount
                calvaryList.Add(placeInCircle(calvaryModel));

            } else {
                // Removes one calvary until the amount on the board matches the calculated amount
                Destroy(calvaryList[0]);
                calvaryList.Remove(calvaryList[0]);

>>>>>>> Stashed changes
            }
        }

        while(infantryList.Count != infantryCount)
        {
            if(infantryList.Count < infantryCount) {
<<<<<<< Updated upstream
                //Adds one calvary until the amount of the board matches the calculated amount

                //Defines a circle and picks a random point inside it to place the troop
                float radius = 2.0f;

                float theta = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; //Random Degree in Radians

                float randX = UnityEngine.Random.Range(0, (radius * Mathf.Cos(theta)) * transform.localScale.x);
                float randZ = UnityEngine.Random.Range(0, (radius * Mathf.Sin(theta)) * transform.localScale.z);
                float newY = 0.2f;

                var dist = new Vector3(randX, newY, randZ);

                infantryList.Add(Instantiate(placeHolderCube, transform.position += dist, transform.rotation, transform));
            } else {
                //Removes one calvary until the amount on the board matches the calculated amount

                Destroy(infantryList[0]);
            }
        }
    }

    //Author: Eoin Howard Scully
    //
    // This method removes a troop from the board
    // (Mid-rewrite)
    void removeTroop()
    {
        //Decrements troop counter  
        numberOfToops -= 1;
        //Detects if there are any 'little' troops that can be removed
        //if not, one 'big' troop will be removed and 9 little troops added
        if (littleTroop.Count > 0) {
            Destroy(littleTroop[0]);
        } else if (bigTroop.Count > 0) {
            Destroy(bigTroop[0]);
            for(int i = 0; i < 10; i++);
            littleTroop.Add(Instantiate(placeHolderCube, transform.position += new Vector3(0, 2, 0), transform.rotation, transform));
        } 
    }
}

// using Troop;
// Troop = new Troop();
// int infantryValue = (int)Troop.TroopTypes.infantry
=======
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
    // 
    void removeTroop(int troopNum = 1)
    {
        // Logs an error if the user tries to remove too many troops
        if (troopNum > numberOfTroops) {
            Debug.LogError("You are trying to remove more troops than exist in this region");
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
    }

    // Author: Eoin Howard Scully
    //
    // This method places one object in a circlular area above the current object
    // It exits to reduce duplicate code
    //
    private GameObject placeInCircle(GameObject objectToPlace) {

        // Defines a circle and picks a random point inside it to place the troop
        float radius = 2.0f;

        float theta = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; // Random Degree in Radians

        float randX = UnityEngine.Random.Range(0, (radius * Mathf.Cos(theta)) * transform.localScale.x);
        float randZ = UnityEngine.Random.Range(0, (radius * Mathf.Sin(theta)) * transform.localScale.z);
        float newY = 50.0f;

        var dist = new Vector3(randX, newY, randZ);

        GameObject newToken = Instantiate(objectToPlace, transform.position += dist, transform.rotation);

        return newToken;
    }

    // Author: Eoin Howard Scully
    //
    // Simple getter method
    // Returns a lists of tags for all the adjacent regions as strings
    //
    List<string> getAdjacentRegions()
    {
        return adjacentRegions;
    }

    // Author: Eoin Howard Scully
    //
    // 'Moves' a certain number of troops from this region to another region
    // Moving is simulated by removing troops here and making new ones in the target region
    //
    void MoveTroops(Region targetRegion, int numOfTroops) 
    {
        if (numOfTroops <= numberOfTroops) {
            removeTroop(numOfTroops);
            targetRegion.addTroop(numOfTroops);
        }
    }
}
>>>>>>> Stashed changes
