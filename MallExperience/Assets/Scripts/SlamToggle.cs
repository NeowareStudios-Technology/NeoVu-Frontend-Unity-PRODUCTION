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
    public Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        high = model.transform.position.y;
        pos = model.transform.position;
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

    public void Remove()
    {
        anchor.enabled = true;
        //reticle = GameObject.FindGameObjectWithTag("Reticle");
        reticle.SetActive(true);
        model.GetComponent<MeshRenderer>().enabled = false;
        Debug.LogWarning(model.GetComponentsInChildren<MeshRenderer>().Length);
        foreach(MeshRenderer child in model.GetComponentsInChildren<MeshRenderer>())
        {
            child.enabled = false;  
        }
    }
}
