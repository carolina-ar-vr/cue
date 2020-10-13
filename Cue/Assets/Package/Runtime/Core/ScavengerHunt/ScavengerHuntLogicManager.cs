using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntLogicManager : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> TrackedObjects;   //Main list of treasure game objects

    [SerializeField]
    private bool RandomizeHunt = false; 

    [SerializeField]
    private bool AllowDuplicates = true; 

    [SerializeField]
    private bool RequireHints = false; 

    void Start()
    {
        if (RandomizeHunt) ShuffleObjects(TrackedObjects); 
    }


    /// <summary>
    /// Set manager to track objects. If require hints is true then validate and track
    /// </summary>
    /// <param name="TrackedObjs"></param>
    public void SetTrackedObjects(GameObject[] TrackedObjs)
    {
        if (RequireHints)
        {
            if (ValidateTrackedObjects(TrackedObjs))
            {
                TrackedObjects = new List<GameObject>(TrackedObjs);
                return;
            }
            else
            {
                Debug.LogError("Require Hints failed check warning to see which objects need the hint component"); 
            }
        }
        TrackedObjects = new List<GameObject>(TrackedObjs);

        if (RandomizeHunt) TrackedObjects = ShuffleObjects(TrackedObjects);
    }

    /// <summary>
    /// Checks if the Object to be tracked have the hint component attached
    /// </summary>
    /// <param name="TrackedObjs">Array of GameObjects to Validate</param>
    /// <returns> True if all objects have hint component else returns false and throws Warnings</returns>
    private bool ValidateTrackedObjects(GameObject[] TrackedObjs)
    {
        bool ObjectsValidated = true;
        foreach(GameObject Obj in TrackedObjs)
        {
            if (!Obj.GetComponent<TreasureHint>())
            {
                Debug.LogWarning(Obj.name + " does not have a TreasureHint Component and RequireHints is set to True");
                ObjectsValidated = false; 
            } 
        }
        return ObjectsValidated; 
    }
    

    /// <summary>
    /// Get array of hints associated with the Tracked Object
    /// </summary>
    /// <param name="TrackedObj"> Game Object to extract hints from</param>
    /// <returns>string[] of hints if TreasureHint is attached otherwise return Null</returns>
    public string[] GetObjectHints(GameObject TrackedObj)
    {
        
        if (TrackedObj.GetComponent<TreasureHint>())
        {
            return TrackedObj.GetComponent<TreasureHint>().GetHints();
        }
        else
        {
            Debug.LogWarning(TrackedObj.name + " does not have a TreasureHint component attached"); 
        }
        
        return null; 
    }

    /// <summary>
    /// Get the current list of tracked objects
    /// </summary>
    /// <returns>List<GameObject> of Tracked Objects</returns>
    public List<GameObject> GetCurrentTrackedObjects()
    {
        return TrackedObjects; 
    }

    /// <summary>
    /// Move Object to front of Tracked Objects
    /// If AllowDuplicates then simply insert Object to front of the List
    /// </summary>
    /// <param name="TrackedObj">GameObject to add to TrackedObject</param>
    public void SetNextTrackedObject(GameObject TrackedObj)
    {
        if (AllowDuplicates)
        {
            TrackedObjects.Insert(0, TrackedObj);
            return; 
        }

        int index = TrackedObjects.IndexOf(TrackedObj);
        TrackedObjects.RemoveAt(index);
        TrackedObjects.Insert(0, TrackedObj); 
    }

    /// <summary>
    /// Move Object to end of Tracked Objects
    /// If AllowDuplicates then simply insert Object to end of the List
    /// </summary>
    /// <param name="TrackedObj"> GameObject to add to Tracked Objects</param>
    public void SetLastTrackedObject(GameObject TrackedObj)
    {
        if (AllowDuplicates)
        {
            TrackedObjects.Insert(TrackedObjects.Count - 1, TrackedObj);
        }

        TrackedObjects.RemoveAt(TrackedObjects.Count - 1);
        TrackedObjects.Insert(TrackedObjects.Count - 1, TrackedObj);

    }

    /// <summary>
    /// Check if the next object is the correct one 
    /// </summary>
    /// <param name="ObjName"> Name of NectObject to Validate</param>
    /// <returns> True if names match</returns>
    public bool ValidateNextObjectByName(string ObjName)
    {
        if (ObjName.Equals(TrackedObjects[0].name)) return true;
        return false;  
    }

    /// <summary>
    /// Get the next Object in line and delete from Tracked Objects
    /// </summary>
    /// <returns>Next Obcjest in line</returns>
    public GameObject GetNextObject()
    {
        if(TrackedObjects.Count == 0) return null; 
        GameObject Obj = TrackedObjects[0]; // Get The topmost treasure 
        TrackedObjects.RemoveAt(0); //Pop from top of the list
        return Obj; 
    }

    /// <summary>
    /// Randomly Shuffles List
    /// </summary>
    /// <typeparam name="E"></typeparam>
    /// <param name="inputList"></param>
    /// <returns>Shuffled List</returns>
    //List Shuffler -> from http://www.vcskicks.com/randomize_array.php
    public List<E> ShuffleObjects<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();
        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }
        return randomList; //return the new random list
    }



}
