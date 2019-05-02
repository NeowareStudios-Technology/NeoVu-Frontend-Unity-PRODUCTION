using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapAutoFocus : MonoBehaviour
{
    public GameObject Camera;
    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("AR Script");   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Debug.Log("Continuous Check");
                        switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.Log("Touch Began");   
                    break;

                case TouchPhase.Ended:
                    Debug.Log("Touch Ended");
                    break;
            }
        }

        if(Input.GetMouseButtonUp(0) == true)
        {
            Debug.Log("Click Released");
            Camera.GetComponent<CameraSettings>().TriggerAutofocusEvent();
        }
    }
}
