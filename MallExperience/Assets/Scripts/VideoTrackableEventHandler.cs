/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
public class VideoTrackableEventHandler : DefaultTrackableEventHandler
{
    public bool animated = false;
    public Animator[] anims;
    #region PROTECTED_METHODS
    protected override void OnTrackingFound()
    {
        mTrackableBehaviour.GetComponentInChildren<VideoController>().Play();
        base.OnTrackingFound();
        if (animated == true)
        {
            for(int i = 0; i<anims.Length; i++)
            {

                anims[i].Play("Main",0,.5f);
            }
        }
        else
        {

        }
    }
    protected override void OnTrackingLost()
    {
        mTrackableBehaviour.GetComponentInChildren<VideoController>().Pause();

        base.OnTrackingLost();
    }

    #endregion // PROTECTED_METHODS
}
