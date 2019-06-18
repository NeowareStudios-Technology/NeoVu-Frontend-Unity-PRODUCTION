using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VuStore : MonoBehaviour
{
    public GameObject storeScreen;
    public GameObject vuFuncManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openStore()
    {
        storeScreen.SetActive(true);
    }
    public void exitStore()
    {
        storeScreen.SetActive(false);
    }
    public void redeemOffer(string costandname)
    {
        
        string[] split = costandname.Split(","[0]);
        int cost = int.Parse(split[0]);
        string offer = (split[1]);
        Debug.Log("Offer cost is: " + cost + " VuPoints");
        Debug.Log("Offer name is: " + offer);
        /*TODO: Check vuPoints
         (if) {
         enough pts redeem
         }*/
        //TODO add QR generation
        vuFuncManager.GetComponent<QREncodeTest>().CodeText = offer;
        vuFuncManager.GetComponent<QREncodeTest>().Encode();
    }
}
