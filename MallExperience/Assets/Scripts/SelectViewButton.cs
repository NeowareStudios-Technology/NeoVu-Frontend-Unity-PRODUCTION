using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectViewButton : MonoBehaviour
{
    public Button button;
    public Text viewName;
    private ScrollList scrollList;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(string currentViewName, ScrollList currentScrollList)
    {
        viewName.text = currentViewName;
        scrollList = currentScrollList;
    }
    
}