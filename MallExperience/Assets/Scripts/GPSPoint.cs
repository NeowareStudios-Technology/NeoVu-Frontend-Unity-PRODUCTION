using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSPoint : MonoBehaviour
{
    public float latitude;
    public float longitude;
    public string type;
    public string Value;
    public GameObject Notification;
    public float bufferLong;
    public float bufferLat;
    public bool reached = false;
    public bool redeemed = false;

    // Start is called before the first frame update
    void Start()
    {
        Notification.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoSomething()
    {
        if (redeemed == false)
        {
            Notification.SetActive(true);
            redeemed = true;
        }
    }
    public void DoNothing()
    {
        Notification.SetActive(false);
        redeemed = false;
    }
}
