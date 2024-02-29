using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Region : MonoBehaviour
{

    // Created by: Eoin Howard Scully
    // This Object represents one region on the game board, it handles the following:
    //   - Selecting a region
    //   - Adding a troop to the region
    //   - Removing a troop from the region  
    //   - Adjacency
    public int numberOfTroops = 0;
    public List<string> adjacentRegions;
    public string continentID;
    public Boolean selected = false;
    public GameObject placeHolderCube;
    private List<GameObject> infantryList;
    private List<GameObject> calvaryList;
    private List<GameObject> artilleryList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Started!");
    }

    // Update is called once per frame
    void Update()
    {   
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
    }

    //called on mouse click
    void OnMouseDown()
    {
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
        
        int infantryVal = (int)TroopTypes.Infantry;
        int calvaryVal = (int)TroopTypes.Cavalry;
        int artilleryVal = (int)TroopTypes.Artillery;

        int totalTroops = troopNum + numberOfTroops;

        int artilleryCount = totalTroops / artilleryVal;
        int calvaryCount = (totalTroops % artilleryVal) / calvaryVal;
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
        }

        while(calvaryList.Count != calvaryCount)
        {
            if(calvaryList.Count < calvaryCount) {
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
            }
        }

        while(infantryList.Count != infantryCount)
        {
            if(infantryList.Count < infantryCount) {
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