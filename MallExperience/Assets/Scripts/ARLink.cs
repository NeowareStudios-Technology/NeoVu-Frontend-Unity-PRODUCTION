﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARLink : MonoBehaviour
{
    public string link;
    public int value;
    public bool inApp = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                Debug.Log("RayCast Sent");
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.LogError("LinkerHit");
                        Debug.LogWarning(this.name);
                        if (inApp == false)
                        {
                            Application.OpenURL(link);
                        }
                        else
                        {
                            InAppBrowser.OpenURL(link);
                        }

                        int playerVal = PlayerPrefs.GetInt("VuPoints");
                        playerVal = playerVal + value;
                        PlayerPrefs.SetInt("VuPoints", playerVal);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("RayCast Sent");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Debug.LogError("LinkerHit");
                    Debug.LogWarning(this.name);
                    if (inApp == false)
                    {
                        Application.OpenURL(link);
                    }
                    else
                    {
                        InAppBrowser.OpenURL(link);
                    }

                    int playerVal = PlayerPrefs.GetInt("VuPoints");
                    playerVal = playerVal + value;
                    PlayerPrefs.SetInt("VuPoints", playerVal);
                }

            }
        }
    }
}

