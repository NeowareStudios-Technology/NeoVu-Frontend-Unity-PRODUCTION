using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamManger : MonoBehaviour
{
    public GameObject productPlacement;
    public GameObject[] items;
    public GameObject currentItem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.touchCount);    
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                Debug.Log("RayCast Sent");
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ARObject")
                {
                    if (productPlacement.GetComponent<ProductPlacement>().chair != hit.transform.gameObject)
                    {
                        productPlacement.GetComponent<ProductPlacement>().chair = hit.transform.gameObject;
                        productPlacement.GetComponent<ProductPlacement>().translationIndicator = hit.transform.gameObject.transform.GetChild(1).gameObject;
                        productPlacement.GetComponent<ProductPlacement>().rotationIndicator = hit.transform.gameObject.transform.GetChild(0).gameObject;
                    }

                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("RayCast Sent");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ARObject")
            {
               if(productPlacement.GetComponent<ProductPlacement>().chair != hit.transform.gameObject)
                {
                    productPlacement.GetComponent<ProductPlacement>().chair = hit.transform.gameObject;
                    productPlacement.GetComponent<ProductPlacement>().translationIndicator = hit.transform.gameObject.transform.GetChild(1).gameObject;
                    productPlacement.GetComponent<ProductPlacement>().rotationIndicator = hit.transform.gameObject.transform.GetChild(0).gameObject;
                }
              
            }
        }
    }

    //Change the target of the product placement and touch handler script with an object from items array
    public void ChangeTarget(int i)
    {
        if (currentItem != null)
        {
            currentItem.transform.GetChild(0).gameObject.SetActive(false); //Set the indicators at the bottom as false
            currentItem.transform.GetChild(1).gameObject.SetActive(false);
        }
        productPlacement.GetComponent<ProductPlacement>().chair = items[i];
        productPlacement.GetComponent<ProductPlacement>().translationIndicator = items[i].transform.GetChild(1).gameObject;
        productPlacement.GetComponent<ProductPlacement>().rotationIndicator = items[i].transform.GetChild(0).gameObject;
        productPlacement.GetComponent<TouchHandler>().augmentationObject = items[i].transform;
        items[i].SetActive(true);
        items[i].gameObject.transform.position = new Vector3(1000, 1000, 100);
        currentItem = items[i];

    }
}
