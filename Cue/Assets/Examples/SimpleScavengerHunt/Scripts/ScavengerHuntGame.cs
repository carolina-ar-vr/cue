using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntGame : MonoBehaviour
{
    // Start is called before the first frame update

    public ScavengerHuntLogicManager myManager;

    public GameObject[] my_Treasure; 
    
    void Start()
    {
        if(my_Treasure != null)
        {
            myManager.SetTresuresToTrack(my_Treasure); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject myTreasure = myManager.GetNextTreasure();

            Debug.Log(myManager.GetTreasureHints(myTreasure)[0]); 

            //Debug.Log(myManager.GetNextTreasure().name);
            //Debug.Log(currG.name); 
        }
    }
}
