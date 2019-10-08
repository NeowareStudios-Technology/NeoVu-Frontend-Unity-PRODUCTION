using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicButton : MonoBehaviour
{
    public AWSManager aws;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void downloadCall()
    {
        aws.DowloadDataSet(this.transform.name.ToLower());
        Debug.LogError(this.transform.name.ToLower());
    }
}
