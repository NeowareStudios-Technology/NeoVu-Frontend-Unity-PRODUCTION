using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VuStore : MonoBehaviour
{
    public GameObject storeScreen;
    public GameObject vuFuncManager;
    public GameObject verify;
    public GameObject notEnoughPoints;
    public TMPro.TextMeshProUGUI verifyText;
    public TMPro.TextMeshProUGUI notEnoughText;
    string currCost;
    public GameObject QRTarg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenStore()
    {
        storeScreen.SetActive(true);
    }
    public void ExitStore()
    {
        storeScreen.SetActive(false);
    }
    public void RedeemOffer()
    {
        string costandname = currCost;
        
        string[] split = costandname.Split(","[0]);
        int cost = int.Parse(split[0]);
        string offer = (split[1]);
        Debug.Log("Offer cost is: " + cost + " VuPoints");
        Debug.Log("Offer name is: " + offer);
        if (PlayerPrefs.GetInt("VuPoints") > cost)
        {
            PlayerPrefs.SetInt("VuPoints", (PlayerPrefs.GetInt("VuPoints") - cost)); // subtract cost from total
            vuFuncManager.GetComponent<QREncodeTest>().CodeText = offer;
            vuFuncManager.GetComponent<QREncodeTest>().Encode();    
        }
        else
        {
            notEnoughPoints.SetActive(true);
        }
    }
    public void VerifyRedemption(string costandName)
    {
        currCost = costandName;
        string[] split = costandName.Split(","[0]);
        int cost = int.Parse(split[0]);
        string offer = (split[1]);
        Debug.Log("Offer cost is: " + cost + " VuPoints");
        Debug.Log("Offer name is: " + offer);
        if (PlayerPrefs.GetInt("VuPoints") > cost)
        {
            verify.SetActive(true);
            verifyText.text = "Are you sure you would like to redeem " + cost + " VuPoints for " + offer + "?";
        }
        else
        {
            notEnoughPoints.SetActive(true);
        }

    }
    public void CloseTab()
    {
        notEnoughPoints.SetActive(false);
        verify.SetActive(false);
        QRTarg.SetActive(false);
    }
}
