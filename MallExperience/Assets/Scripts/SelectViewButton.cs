using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectViewButton : MonoBehaviour
{
    public AWSManager awsm;
    public Button button;
    public Text viewName;
    private ScrollList scrollList;

    // Start is called before the first frame update
    void Start()
    {
        awsm = GameObject.Find("AWS_Manager").GetComponent<AWSManager>();
    }

    public void Setup(string currentViewName, ScrollList currentScrollList)
    {
        viewName.text = currentViewName;
        scrollList = currentScrollList;
    }

    public void StartAssetDownloads()
    {
        awsm.DowloadDataSet(viewName.text);
    }
    
}