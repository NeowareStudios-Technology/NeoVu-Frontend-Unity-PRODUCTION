using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSwitch : MonoBehaviour
{
    public GameObject ball;
    public GameObject slam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SlamOn()
    {
        slam.SetActive(true);
        ball.SetActive(false);
    }

    public void SlamOff()
    {
        ball.SetActive(true);
        slam.SetActive(false);
    }
}
