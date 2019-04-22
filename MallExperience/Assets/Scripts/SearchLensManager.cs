using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchLensManager : MonoBehaviour
{
    public LensListJSON llj;
    public ScrollList sl;
    public InputField searchInput;
    public Text outputText;

    public void SearchMatchingLenses()
    {
        sl.RemoveButtons();

        outputText.text = "";
        int outputCount = 0;
        foreach(string lensName in llj.lenses)
        {
            if (searchInput.text != "")
            {
                if(lensName.Contains(searchInput.text))
                {
                    sl.AddButton(lensName);
                }
            }
        }

        if ((outputCount == 0) || (searchInput.text != ""))
        {
            Debug.Log("No matches found");
        }
    }
}
