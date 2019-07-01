using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuPointHandler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI vuPointsVal;
    public TMPro.TextMeshProUGUI vuPointsReponse;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("VuPoints") == false)
        {
            Debug.Log("VuPoints Value not found");
            PlayerPrefs.SetInt("VuPoints", 0);
            Debug.Log("VuPoints Created");
        }
        else
        {
            Debug.Log("VuPointsFound");
        }
    }

    // Update is called once per frame
    void Update()
    {
        vuPointsVal.text = (PlayerPrefs.GetInt("VuPoints").ToString());
    }

    public void addPoints(int value)
    {
        int playerVal = PlayerPrefs.GetInt("VuPoints");
        playerVal = playerVal + value;
        PlayerPrefs.SetInt("VuPoints", playerVal);
    }
    public void Purchase(int value)
    {
        int playerVal = PlayerPrefs.GetInt("VuPoints");
        if (value > playerVal)
        {
            vuPointsReponse.text = "You do not have enough VuPoints";
        }
        else
        {
            playerVal = playerVal - value;
            PlayerPrefs.SetInt("VuPoints", playerVal);
            vuPointsReponse.text = "Purchase Confirmed";
        }
    }
}
