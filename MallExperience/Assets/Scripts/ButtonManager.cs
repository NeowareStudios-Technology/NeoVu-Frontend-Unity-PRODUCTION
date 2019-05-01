using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject[] buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Disable flashlight when called
    public void buttonsDisable() 
    {
       for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
            Debug.Log("button " + i + " Disabled");
        }
    }

    //Enable flashlight when called
    public void buttonsEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(true);
        }
    }
}
