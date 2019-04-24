using UnityEngine;

public class ViewSceneSwipeManager : MonoBehaviour
{
    public PanelOpener qrOpener;
    public PanelOpener adOpener;


    private bool onlySwipeRight = false;
    private bool onlySwipeUp = false;
    private bool restrictToQr = false;
    private bool restrictToAd = false;

    private void Awake()
    {
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipeLeft;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipeRight;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipeDown;
        SwipeDetector.OnSwipe += SwipeDetector_OnSwipeUp;
    }

    private void SwipeDetector_OnSwipe(SwipeData data)
    {
        Debug.Log("Swipe in Direction: " + data.Direction);
    }
    private void SwipeDetector_OnSwipeLeft(SwipeData data)
    {
        if(data.Direction == SwipeDirection.Left && onlySwipeRight == false && restrictToAd == false)
        {
            onlySwipeRight = true;
            restrictToQr = true;
            qrOpener.OpenPanel();
        }
    }
    private void SwipeDetector_OnSwipeRight(SwipeData data)
    {
        if (data.Direction == SwipeDirection.Right)
        {
            onlySwipeRight = false;
            restrictToQr = false;
            qrOpener.ClosePanel();
        }
    }
    private void SwipeDetector_OnSwipeDown(SwipeData data)
    {
        if (data.Direction == SwipeDirection.Down && onlySwipeUp == false && restrictToQr == false)
        {
            onlySwipeUp = true;
            restrictToAd = true;
            adOpener.OpenPanel();
        }
    }
    private void SwipeDetector_OnSwipeUp(SwipeData data)
    {
        if (data.Direction == SwipeDirection.Up)
        {
            onlySwipeUp = false;
            restrictToAd = false;
            adOpener.ClosePanel();
        }
    }

    public void openOrCloseAd()
    {
        if (restrictToAd == true)
        {
            onlySwipeUp = false;
            restrictToAd = false;
            adOpener.ClosePanel();
        }
        else if(restrictToQr == false)
        {
            onlySwipeUp = true;
            restrictToAd = true;
            adOpener.OpenPanel();
        }
    }
    public void openOrCloseQr()
    {
        if (restrictToQr == true)
        {
            onlySwipeRight = false;
            restrictToQr = false;
            qrOpener.ClosePanel();
        }
        else if (restrictToAd == false)
        {
            onlySwipeRight = true; 
            restrictToQr = true;
            qrOpener.OpenPanel();
        }
    }



}