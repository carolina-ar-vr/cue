using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System;
using System.Linq;

public class ARScavengerHunt : MonoBehaviour
{
    // Start is called before the first frame update

    public ARTrackedImageManager imgManager;
    public XRReferenceImageLibrary m_ImageLibrary;
    static List<Guid> image_guids = new List<Guid>(); 
    private ScavengerHuntLogicManager scavengerHuntManager; //Logic Manager
    public List<GameObject> my_Treasures; //Treasures to track

    private List<GameObject> SpawnedTreasures = new List<GameObject>(); 
    public bool firstTracked = false;

    public TextMesh debugger; 

    GameObject CurrentObject; 


    void Start()
    {
        imgManager = GetComponent<ARTrackedImageManager>();
        scavengerHuntManager = GetComponent<ScavengerHuntLogicManager>();
        Debug.Log(m_ImageLibrary.count); 
    }

    void OnEnable()
    {
        
        for(int i = 0; i < m_ImageLibrary.count; i++)
        {
            image_guids.Add(m_ImageLibrary[i].guid); 
        }
        imgManager.trackedImagesChanged += ImageManagerOnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imgManager.trackedImagesChanged -= ImageManagerOnTrackedImagesChanged;
    }

    void ImageManagerOnTrackedImagesChanged(ARTrackedImagesChangedEventArgs obj)
    {
        // added, spawn prefab
        foreach (ARTrackedImage image in obj.added)
        {
            if (image_guids.Contains(image.referenceImage.guid))
            {
                debugger.text= "Image MATCHED: " + image.referenceImage.name;
                Debug.Log("Image MATCHED: " + image.referenceImage.name);
                if (firstTracked == false)
                {
                    debugger.text = "First Image: " + image.referenceImage.name;
                    Debug.Log("First Image: " + image.referenceImage.name);
                    CurrentObject = my_Treasures.Where(GameObject => GameObject.name == image.referenceImage.name).SingleOrDefault();
                    if(CurrentObject != null)
                    {
                        GameObject SpawnObj = Instantiate(CurrentObject, image.transform.position, image.transform.rotation);
                        SpawnedTreasures.Add(SpawnObj);
                        debugger.text = "First Image Spawn";
                        Debug.Log("First Image Spawn: " + SpawnObj.name);

                        my_Treasures.Remove(CurrentObject);
                        scavengerHuntManager.SetTrackedObjects(my_Treasures.ToArray());
                        debugger.text = "Scavenger Hunt Initiated"; 
                        CurrentObject = scavengerHuntManager.GetNextObject();
                        debugger.text = "Got First Next Obj";
                        Debug.Log("Next Object: " + CurrentObject.name);
                        firstTracked = true;

                    }
                   
                }

                if (CurrentObject.name == image.referenceImage.name)
                {
                    debugger.text = "lOOKIN AT CORRENT NECT IMAGE";
                    Debug.Log("Next Object: " + CurrentObject.name);
                    GameObject sObj = Instantiate(CurrentObject, image.transform.position, image.transform.rotation);
                    debugger.text = "Instanciate Successful";
                    SpawnedTreasures.Add(sObj);
                    CurrentObject = scavengerHuntManager.GetNextObject();
                }

                
            }
        }

        // updated, set prefab position and rotation
        foreach (ARTrackedImage image in obj.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (image.trackingState == TrackingState.Tracking)
            {
                if (image_guids.Contains(image.referenceImage.guid))
                {
                    GameObject gObj = SpawnedTreasures.Where(GameObject => GameObject.name == image.referenceImage.name).SingleOrDefault();
                    if(gObj != null)
                    {
                        gObj.SetActive(true);
                        gObj.transform.SetPositionAndRotation(image.transform.position, image.transform.rotation);
                    }
                }
            }
            else
            {
                if (image_guids.Contains(image.referenceImage.guid))
                {
                    GameObject gObj = SpawnedTreasures.Where(GameObject => GameObject.name == image.referenceImage.name).SingleOrDefault();
                    if(gObj != null)gObj.SetActive(false);
                }
            }
        }

        // removed, destroy spawned instance
        foreach (ARTrackedImage image in obj.removed)
        {
            if (image_guids.Contains(image.referenceImage.guid))
            {
                GameObject gObj = SpawnedTreasures.Where(GameObject => GameObject.name == image.referenceImage.name).SingleOrDefault();
                SpawnedTreasures.Remove(gObj);
                Destroy(gObj); 
            }

        }
    }
}
