using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class ViewInfo
{
    public string viewName;
}

public class ScrollList : MonoBehaviour {

    public List<ViewInfo> viewInfoList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    


    // Use this for initialization
    void Start () 
    {
        RefreshDisplay ();
    }

    void RefreshDisplay()
    {
        RemoveButtons ();
        AddButtons ();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0) 
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < viewInfoList.Count; i++) 
        {
            ViewInfo viewInfo = viewInfoList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(viewInfo, this);
        }
    }
}
