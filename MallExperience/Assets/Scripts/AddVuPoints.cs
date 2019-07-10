using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVuPoints : MonoBehaviour
{
    public bool repeatable = false; //Set if the 
    public bool certainRepeat = false;
    public int repeatAmount;
    public int addAmount;
    public bool redeemed = false;
    public int timesRedeemed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Add()
    {
        if(repeatable == true)
        {
            if(certainRepeat  == true)
            {

            }
            else
            {
                PlayerPrefs.SetInt("VuPoints", PlayerPrefs.GetInt("VuPoints") + addAmount);
            }
        }
        else
        {
            if(redeemed == false)
            {
               PlayerPrefs.SetInt("VuPoints", PlayerPrefs.GetInt("VuPoints") + addAmount);
               redeemed = true;
            }
            else
            {

            }
        }
    }
}
