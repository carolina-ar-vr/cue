using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScavengerHuntGame : MonoBehaviour
{

    public ScavengerHuntLogicManager myManager; //Logic Manager
    public GameObject[] my_Treasures; //Treasures to track
    public Text Hints;
    public Text Status; 

    private GameObject currentObj; 

    void Start()
    {
        if(my_Treasures != null)
        {
            myManager.SetTrackedObjects(my_Treasures);
        }

        currentObj = myManager.GetNextObject();
        Hints.text = myManager.GetObjectHints(currentObj)[0];
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(currentObj.name == "CapsuleTreasure")
            {
                Status.text = "You Choose Correctly";
                currentObj = myManager.GetNextObject();
                if(currentObj != null) Hints.text = "Hint: " +     myManager.GetObjectHints(currentObj)[0];
            }
            else
            {
                Status.text = "Choose Again";
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentObj.name == "CubeTreasure")
            {
                Status.text = "You Choose Correctly";
                currentObj = myManager.GetNextObject();
                if (currentObj != null) Hints.text = "Hint: " + myManager.GetObjectHints(currentObj)[0];
            }
            else
            {
                Status.text = "Choose Again";
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (currentObj.name == "CylinderTreasure")
            {
                Status.text = "You Choose Correctly";
                currentObj = myManager.GetNextObject();
                if (currentObj != null) Hints.text = "Hint: "+ myManager.GetObjectHints(currentObj)[0];
            }
            else
            {
                Status.text = "Choose Again";
            }
        }

        if(currentObj == null)
        {
            Status.text = "You won";
        }
    }
}
