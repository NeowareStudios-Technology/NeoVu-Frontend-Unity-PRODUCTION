using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;

public class SDCardDataSetLoader : MonoBehaviour {
     private bool mLoaded = false;
     private DataSet mDataset = null;
    public GameObject dataHold;
    // Update is called once per frame
    private void Start()
    {
        dataHold = GameObject.Find("Data");
    }
    void Update () {
        if(dataHold == null)
        {
            dataHold = GameObject.Find("Data");
        }
          if (VuforiaRuntimeUtilities.IsVuforiaEnabled() && !mLoaded) {
          string externalPath = dataHold.GetComponent<DataSetHolder>().XMLHolder;

          if (mDataset == null) {
          // First, create the dataset
          ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
          mDataset = tracker.CreateDataSet();
          }
         
         if (mDataset.Load(externalPath, VuforiaUnity.StorageType.STORAGE_ABSOLUTE)) {
            mLoaded = true;
                Debug.LogError("DataLoaded");
            }
         else {
           Debug.LogError ("Failed to load dataset!");
         }
}
}
}
