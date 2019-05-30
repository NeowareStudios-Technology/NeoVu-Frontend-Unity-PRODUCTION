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
    public GameObject[] ARButtons;
    public GameObject SlamManager;
    public GameObject GroundPlaneUi;
    // Start is called before the first frame update
    void Start()
    {
        buttons = GameObject.FindGameObjectsWithTag("Button");
        ARButtons = GameObject.FindGameObjectsWithTag("ARUI");
        //ButtonBlock.transform.localPosition = new Vector3(0, -0.016f, 0.077f);
        //ButtonBlock.transform.localScale = new Vector3(0.007f, 1e-05f, 0.007f);
        if (ARButtons.Length > 0)
        {
            for (int i = 0; i < ARButtons.Length; i++)
            {
                ARButtons[i].SetActive(false);
            }
        }
        productPlacement.SetActive(false);
        PlaneManager.SetActive(false);
        GroundPlaneUi.SetActive(false);
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
        GroundPlaneUi.SetActive(true);
        if (SwipeScript != null)
        {
            SwipeScript.GetComponent<SwipeDetector>().gameObject.SetActive(false);
        }
        buttonsDisable();
        for (int i = 0; i < ARButtons.Length; i++)
        {
            ARButtons[i].SetActive(true);
        }
        ARbutton.SetActive(false);
        CameraButton.SetActive(true);
        productPlacement.GetComponent<ProductPlacement>().chair.gameObject.GetComponent<MeshCollider>().enabled = true;

    }

    public void CameraMode()
    {
        productPlacement.GetComponent<TouchHandler>().active = false;
        productPlacement.SetActive(false);
        PlaneManager.SetActive(false);
        GroundPlaneUi.SetActive(false);
        if (SwipeScript != null)
        {
            SwipeScript.GetComponent<SwipeDetector>().gameObject.SetActive(true);
        }
        buttonsEnable();
        ARbutton.SetActive(true);
        CameraButton.SetActive(false);
        for (int i = 0; i < ARButtons.Length; i++)
        {
            ARButtons[i].SetActive(false);
        }
        //Turns off the translation and rotation indicators on slam objects
        if (SlamManager != null)
        {
            for (int j = 0; j < SlamManager.GetComponent<SlamManger>().items.Length; j++)
            {
                SlamManager.GetComponent<SlamManger>().items[j].transform.GetChild(0).gameObject.SetActive(false);
                SlamManager.GetComponent<SlamManger>().items[j].transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}
