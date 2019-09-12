/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.Video;
public class VideoTrackableEventHandler : DefaultTrackableEventHandler
{
    public bool animated = false;
    public Animator[] anims;
    public YoutubePlayer player;
    public VideoPlayer[] VPlayer;
    public VideoPlayer video;
    #region PROTECTED_METHODS
    
    protected override void OnTrackingFound()
    {
        //mTrackableBehaviour.transform.GetChild(0).gameObject.SetActive(true);
        player.Play();
        video.enabled = true;
        for(int i = 0; i < VPlayer.Length; i++)
        {
            VPlayer[i].Play();
        }
        //player.enabled = true;
        //mTrackableBehaviour.GetComponentInChildren<VideoController>().Play();
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
        player.Pause();
        for (int i = 0; i < VPlayer.Length; i++)
        {
            VPlayer[i].Pause();
        }
        //player.enabled = false;
        //mTrackableBehaviour.GetComponentInChildren<VideoController>().Pause();
        // mTrackableBehaviour.transform.GetChild(0).gameObject.SetActive(false);
        base.OnTrackingLost();

    }

    #endregion // PROTECTED_METHODS
}
