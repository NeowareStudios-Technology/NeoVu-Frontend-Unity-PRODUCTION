using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayOpener : MonoBehaviour
{
    public GameObject baseCanvas;
    public GameObject newCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openCanvas()
    {
        baseCanvas.SetActive(false);
        newCanvas.SetActive(true);
    }
    public void closeCanvas()
    {
        baseCanvas.SetActive(true);
        newCanvas.SetActive(false);
    }
}
