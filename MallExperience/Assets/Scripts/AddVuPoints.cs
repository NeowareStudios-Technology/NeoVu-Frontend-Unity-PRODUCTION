using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddVuPoints : MonoBehaviour
{
    public bool limited = false; //Set if the 
    public int limitAmount; // the amount of times the reward can be redeemed before locking out
    public int addAmount;
    public string SaveName;
    public Animator anim;
    public TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning(PlayerPrefs.GetInt(SaveName));
        Debug.LogWarning(PlayerPrefs.GetInt("VuPoints"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add()
    {
        if (limited == true)
        {
            if(PlayerPrefs.GetInt(SaveName) < limitAmount)
            {
                PlayerPrefs.SetInt(SaveName, PlayerPrefs.GetInt(SaveName) + 1);
                Debug.LogWarning(PlayerPrefs.GetInt(SaveName));
                PlayerPrefs.SetInt("VuPoints", PlayerPrefs.GetInt("VuPoints") + addAmount);
                Debug.LogWarning(PlayerPrefs.GetInt("VuPoints"));
                text.text = "You Received: " + addAmount + " VuPoints!";
                anim.Play("Main");
            }
            else
            {
                //play out an already redeemed animation 
                Debug.LogWarning("Limit Reached");
            }
        }
        else
        {
            Debug.LogWarning("Add: " + addAmount + " Vupoints");
            PlayerPrefs.SetInt("VuPoints", PlayerPrefs.GetInt("VuPoints") + addAmount);
            Debug.LogWarning(PlayerPrefs.GetInt("VuPoints"));
            text.text = "You Received: " + addAmount + " VuPoints!";
            anim.Play("Main");
        }
    }
}
