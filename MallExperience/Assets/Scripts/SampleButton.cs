using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{
    public Button button;
    public Text viewName;

    private ViewInfo viewInfo;
    private ScrollList scrollList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(ViewInfo currentViewInfo, ScrollList currentScrollList)
    {
        viewInfo = currentViewInfo;
        viewName.text = viewInfo.viewName;

        scrollList = currentScrollList;
    }
    
}
