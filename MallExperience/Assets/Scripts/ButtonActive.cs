using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{

    public GPSPoint connector;
    public GameObject[] offer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(connector.reached == true)
        {
            for (int i = 0; i < offer.Length; i++)
            {
                offer[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < offer.Length; i++)
            {
                offer[i].SetActive(false);
            }
        }
    }
}
