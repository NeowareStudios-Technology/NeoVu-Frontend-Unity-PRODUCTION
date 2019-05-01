using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject flashlight;
    // Start is called before the first frame update
    void Start()
    {
        flashlight = GameObject.Find("Flashlight"); //find glashlight/torch button at runtime
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Disable flashlight when called
    public void flashDisable() 
    {
        flashlight.SetActive(false);
    }

    //Enable flashlight when called
    public void flashEnable()
    {
        flashlight.SetActive(true);
    }
}
