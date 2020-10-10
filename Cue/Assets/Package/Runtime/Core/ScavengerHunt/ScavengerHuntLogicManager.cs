using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntLogicManager : MonoBehaviour
{


    [SerializeField]
    private List<GameObject> Treasures;   //Main list of treasure game objects

    [SerializeField]
    private bool RandomProgression; //Default is false for progression type


    

    private Dictionary<string, GameObject> TreasuretoNameMap; //Store Dict with gameobject and name

    void Start()
    {
        SetUp();
    }


    void SetUp()
    {
        //If random Progression then shuffle List
        if (RandomProgression)
        {
            Treasures = ShuffleList(Treasures); 
        }

        TreasuretoNameMap = new Dictionary<string, GameObject>(); 
        //Map Treasure to Name
        foreach (GameObject obj in Treasures)
        {
            
            TreasuretoNameMap[obj.name] = obj; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Shuffle List

   
    public string[] GetTreasureHints(GameObject treasure)
    {

        if (treasure.GetComponent<TreasureHint>())
        {
            return treasure.GetComponent<TreasureHint>().GetHints(); 
        }

        return null; 
    }

    
    public List<GameObject> GetTreasureList()
    {
        return Treasures; 
    }


    public void SetNextTreasure(GameObject treasure)
    {
        Treasures.Insert(0, treasure); 
    }


    public void SetLastTresure(GameObject treasure)
    {
        Treasures.Insert(Treasures.Count - 1, treasure); 
    }


    public GameObject GetNextTreasure()
    {

        if(Treasures.Count == 0)return null; 
        
        GameObject NextTreasure = Treasures[0]; // Get The topmost treasure 
        Treasures.RemoveAt(0); //Pop from top of the list

        //TODO MAYBE: Get hint from the next object and set as hint of the curent object
        return NextTreasure; 
    }


    public void SetTresuresToTrack(GameObject[] TreasureObjs)
    {
        Treasures = new List<GameObject>(TreasureObjs); 
    }
   

    //TODO: Return Object by name in the list otherwise return null
    public GameObject GetTreasure(string treasureName)
    {
        return null; 
    }

    


    //List Shuffler -> from http://www.vcskicks.com/randomize_array.php
    private List<E> ShuffleList<E>(List<E> inputList)
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
