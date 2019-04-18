﻿/************************************
 * Project: NoWare Lens
 * Author: David Lee Ramirez
 * Date: 4/16/19
 * Copywrite NeoWare 2019
 * *********************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using System.Linq;

public class VuforiaSetupManager : MonoBehaviour
{
	//put the names of all your "target object" GameObjects here
	public string[] targetObjectName;// = new string[] {"TestCube"};
    public GameObject[] targetObjects;
	//put name of dataset here
    public string dataSetName;// = "Business2";
	private int targetObjectNameCount;
	private int targetCount = 0;

    // Start is called before the first frame update
    void Start()
    {
		Debug.Log("step 1");
		//make array of GameObjects for each model named
        targetObjects = new GameObject[targetObjectName.Length];

		//find the GameObject corresponding to each model name
		targetObjectNameCount = 0;
		foreach(string name in targetObjectName)
		{
        	targetObjects[targetObjectNameCount] = GameObject.Find(targetObjectName[targetObjectNameCount]);
			targetObjectNameCount++;
		}
		Debug.Log("step 2");

		//VuforiaARController.Instance.UpdateState(true,true);
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(ActivateDatasetFromStreamingAssets);

		Debug.Log("step 3");
    }


    private void ActivateDatasetFromStreamingAssets()
    {
        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();
		IEnumerable<DataSet> dataSetList = objectTracker.GetActiveDataSets();
  
		foreach(DataSet set in dataSetList.ToList()){
			objectTracker.DeactivateDataSet(set);
		}

		// Check if the data set exists at the given path.
		if (!DataSet.Exists(dataSetName))
		{
			Debug.LogError("Data set does not exist.");
        }

        // Create a new empty data set.
		DataSet dataSet = objectTracker.CreateDataSet();
	
		// Load the data set from the given path.
		if (!dataSet.Load(dataSetName))
		{
			Debug.LogError("Failed to load data set ");
		}
        else
        {
            Debug.Log("Dataset loaded successfully");
        }
	
		// (Optional) Activate the data set.
		objectTracker.ActivateDataSet(dataSet);
		objectTracker.Start();

        AttachContentToTrackables(dataSet, targetObjects);
    }


    // Add Trackable event handler and target objects to targets
	private void AttachContentToTrackables(DataSet dataSet, GameObject[] targetObjects)
	{
		// get all current TrackableBehaviours
		IEnumerable<TrackableBehaviour> trackableBehaviours =
		TrackerManager.Instance.GetStateManager().GetTrackableBehaviours();
        targetCount = 0;

		// Loop over all TrackableBehaviours.
		foreach (TrackableBehaviour trackableBehaviour in trackableBehaviours)
		{
			// check if the Trackable of the current Behaviour is part of this dataset
			if (dataSet.Contains(trackableBehaviour.Trackable))
			{
				GameObject go = trackableBehaviour.gameObject;

				// Add a Trackable event handler to the Trackable.
				// This Behaviour handles Trackable lost/found callbacks.
				go.AddComponent<DefaultTrackableEventHandler>();

				// Attach the cube to the Trackable and make sure it has a proper size.
				targetObjects[targetCount].transform.parent = trackableBehaviour.transform;
				targetObjects[targetCount].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
				targetObjects[targetCount].transform.localPosition = new Vector3(0.0f, 0.35f, 0.0f);
				targetObjects[targetCount].transform.localRotation = Quaternion.identity;
				targetObjects[targetCount].active = true;
				trackableBehaviour.gameObject.active = true;

                targetCount++;
			}
		}
    }
}