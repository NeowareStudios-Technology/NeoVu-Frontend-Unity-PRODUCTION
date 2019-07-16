/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.Video;
public class VuPointTrackable : DefaultTrackableEventHandler
{
    public AddVuPoints Adder;
    #region PROTECTED_METHODS
    
    protected override void OnTrackingFound()
    {
        Adder.Add();
        base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        //player.enabled = false;
        //mTrackableBehaviour.GetComponentInChildren<VideoController>().Pause();
       // mTrackableBehaviour.transform.GetChild(0).gameObject.SetActive(false);
        base.OnTrackingLost();
    }

    #endregion // PROTECTED_METHODS
}
