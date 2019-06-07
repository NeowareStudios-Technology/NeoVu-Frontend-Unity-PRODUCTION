using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if PLATFORM_IOS
using UnityEngine.iOS;
#endif
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif


public class GPS : MonoBehaviour
{
    public TMPro.TextMeshProUGUI debugText;
    public TMPro.TextMeshProUGUI fatLat;
    public TMPro.TextMeshProUGUI wrongLong;
    public float targLat;
    public float targLong;
    public TMPro.TextMeshProUGUI targetLong;
    public TMPro.TextMeshProUGUI TargetLat;
    public TMPro.TextMeshProUGUI Results;
    private int timeCheck;
    private bool latCheck = false;
    private bool longCheck = false;
    public bool reward;
    public bool redeemed = false;
    public int value;

    public void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif
        targetLong.text = targLong.ToString();
        TargetLat.text = (targLat - .00003f).ToString();
        StartCoroutine(checkLocation());
    }

    IEnumerator checkLocation()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
        /*#if PLATFORM_ANDROID
            Debug.Log("Location Services Not Enabled");
            StartCoroutine(checkLocation());
            yield break;
        #endif*/
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Time til Test " + maxWait + " Seconds");
            debugText.text = ("Time til Test " + maxWait + " Seconds");
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            debugText.text = ("Timed out");
            StartCoroutine(checkLocation());
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            debugText.text = ("Unable to determine device location");
            StartCoroutine(checkLocation());
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            fatLat.text = ("Latitude: " + Input.location.lastData.latitude);
            wrongLong.text = ("Longitude: " + Input.location.lastData.longitude);
            LocationVerify();
            StartCoroutine(Retry());
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();


    }
    IEnumerator Retry()
    {
        int maxWait = 10;

        while (maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Time til Test " + maxWait + " Seconds");
            debugText.text = ("Time til Test " + maxWait + " Seconds");
        }
        if (maxWait < 1)
        {
            StartCoroutine(checkLocation());
        }
    }
    public void LocationVerify()
    {
        if ((Input.location.lastData.latitude >= (targLat - .00003f)) && (Input.location.lastData.latitude <= (targLat + .00003f)))
        {
            Results.text = ("Latitude in Range,  time: " + timeCheck.ToString());
            latCheck = true;
        }
        else
        {
            latCheck = false;
        }


         if((Input.location.lastData.longitude >= (targLong - .00003f)) && (Input.location.lastData.longitude <= (targLong + .00003f)))
        {
            Results.text = ("longitude match, time: " + timeCheck.ToString());
            longCheck = true;
        }
        else
        {
            longCheck = false;
        }

         if(latCheck == true && longCheck == true)
        {
            Results.text = ("Target Location in Range, time: " + timeCheck.ToString());
            if(reward == true && redeemed == false)
            {
                this.gameObject.GetComponent<VuPointHandler>().addPoints(value);
                redeemed = true;
            }
        }
        timeCheck++;
    }
}