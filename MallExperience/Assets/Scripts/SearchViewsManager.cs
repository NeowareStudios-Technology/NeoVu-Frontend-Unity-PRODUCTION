using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchViewsManager : MonoBehaviour
{
    public AWSManager awsm;
    public ScrollList sl;
    public InputField searchInput;

    public void SearchMatchingViews()
    {
        sl.RemoveButtons();

        int outputCount = 0;
        //for each view in the list of views downloaded from s3
        foreach(string viewName in awsm.vlj.views)
        {
            if (searchInput.text != "")
            {
                if(viewName.Contains(searchInput.text.ToLower()))
                {
                    sl.AddButton(viewName);
                }
            }
        }

        if ((outputCount == 0) || (searchInput.text != ""))
        {
            Debug.Log("No matches found");
        }
    }
}