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
    //public TMPro.TextMeshProUGUI debugText;
    //public TMPro.TextMeshProUGUI fatLat;
    //public TMPro.TextMeshProUGUI wrongLong;
    //public TMPro.TextMeshProUGUI targetLong;
    // public TMPro.TextMeshProUGUI TargetLat;
    //public TMPro.TextMeshProUGUI Results;
    private int timeCheck;
    private bool latCheck = false;
    private bool longCheck = false;
    public bool reward;
    public bool redeemed = false;
    public int value;
    public bool newExperience = false;
    public GPSPoint[] gpsPoints;
    private float lastLat = 0.0f;
    private float lastLong = 0.0f;

    public void Start()
    {

#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
#endif

#if PLATFORM_IOS
        if (!Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }

#endif
        //targetLong.text = targLong.ToString();
        //TargetLat.text = (targLat - .00003f).ToString();
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
        int maxWait = 2;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Time til Test " + maxWait + " Seconds");
            //debugText.text = ("Time til Test " + maxWait + " Seconds");
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            // debugText.text = ("Timed out");
            StartCoroutine(checkLocation());
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //debugText.text = ("Unable to determine device location");
            StartCoroutine(checkLocation());
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            //fatLat.text = ("Latitude: " + Input.location.lastData.latitude);
            //wrongLong.text = ("Longitude: " + Input.location.lastData.longitude);
            if (Input.location.lastData.latitude != lastLat)
            {
                lastLat = Input.location.lastData.latitude;
                LocationVerify();
                StartCoroutine(Retry());
            }
            else
            {
                StartCoroutine(Retry());
            }
        }

        // Stop service if there is no need to query location updates continuously
        Input.location.Stop();


    }
    IEnumerator Retry()
    {
        int maxWait = 2;

        while (maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Time til Test " + maxWait + " Seconds");
            //debugText.text = ("Time til Test " + maxWait + " Seconds");
        }
        if (maxWait < 1)
        {
            StartCoroutine(checkLocation());
        }
    }
    public void LocationVerify()
    {
        Debug.LogWarning("Location Verify");
        for (int i = 0; i < gpsPoints.Length; i++)
        {
            latCheck = false;
            longCheck = false;
            if ((Input.location.lastData.latitude >= (gpsPoints[i].latitude - gpsPoints[i].bufferLat)) && (Input.location.lastData.latitude <= (gpsPoints[i].latitude + gpsPoints[i].bufferLat)))
            {
                //Results.text = ("Latitude in Range,  name: " + gpsPoints[i].Value);
                latCheck = true;
            }
            else
            {
                latCheck = false;
            }


            if ((Input.location.lastData.longitude >= (gpsPoints[i].longitude - gpsPoints[i].bufferLong)) && (Input.location.lastData.longitude <= (gpsPoints[i].longitude + gpsPoints[i].bufferLong)))
            {
                //Results.text = ("longitude match, name: " + gpsPoints[i].Value);
                longCheck = true;
            }
            else
            {
                longCheck = false;
            }
            if (latCheck == true && longCheck == true)
            {
                //Results.text = ("Target Location in Range, Name: " + gpsPoints[i].Value);
                gpsPoints[i].DoSomething();

            }
            else
            {
                latCheck = false;   //This resets both checks to avoid a possible albeit very unlikely overlap between seperate points
                longCheck = false;
                gpsPoints[i].DoNothing();
            }
        }
        timeCheck++;
    }
}