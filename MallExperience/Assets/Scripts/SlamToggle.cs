using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SlamToggle : MonoBehaviour
{
    public AnchorInputListenerBehaviour anchor;
    public GameObject reticle;
    public GameObject model;
    public float high;
    // Start is called before the first frame update
    void Start()
    {
        high = model.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place()
    {
        anchor.enabled = false;
        reticle = GameObject.FindGameObjectWithTag("Reticle");
        reticle.SetActive(false);
    }
}
