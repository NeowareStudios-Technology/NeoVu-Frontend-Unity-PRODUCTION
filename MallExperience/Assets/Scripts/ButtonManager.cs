using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject[] buttons;
    public GameObject productPlacement;
    public GameObject PlaneManager;
    public GameObject SwipeScript;
    public GameObject ARbutton;
    public GameObject CameraButton;
    public GameObject ButtonBlock;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        ButtonBlock.transform.localPosition = new Vector3(0, -0.016f, 0.077f);
        ButtonBlock.transform.localScale = new Vector3(0.007f, 1e-05f, 0.007f);
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

    public void ARMode()
    {
        productPlacement.GetComponent<TouchHandler>().active = true;
        productPlacement.SetActive(true);
        PlaneManager.SetActive(true);
        SwipeScript.GetComponent<SwipeDetector>().gameObject.SetActive(false);
        buttonsDisable();
        ARbutton.SetActive(false);
        CameraButton.SetActive(true);

    }

    public void CameraMode()
    {
        productPlacement.GetComponent<TouchHandler>().active = false;
        productPlacement.SetActive(false);
        PlaneManager.SetActive(false);
        SwipeScript.GetComponent<SwipeDetector>().gameObject.SetActive(true);
        buttonsEnable();
        ARbutton.SetActive(true);
        CameraButton.SetActive(false);
    }
}
