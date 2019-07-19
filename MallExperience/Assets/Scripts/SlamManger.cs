using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlamManger : MonoBehaviour
{
    public GameObject productPlacement;
    public GameObject[] items;
    public GameObject currentItem;
    public bool delete;
    public GameObject indicator;
    public Text debugText;
    public Text touchCounter;
    private bool firstTime = false;
    public bool multiObject = false;
    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("Indicator");
    }

    // Update is called once per frame
    void Update()
    {
        if (productPlacement.GetComponent<ProductPlacement>().chair != null)
        {
            debugText.text = productPlacement.GetComponent<ProductPlacement>().chair.name;
        }
        touchCounter.text = Input.touchCount.ToString();
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
                    if (productPlacement.GetComponent<ProductPlacement>().chair != hit.transform.gameObject && delete == false)
                    {
                        productPlacement.GetComponent<ProductPlacement>().chair = hit.transform.gameObject;
                        productPlacement.GetComponent<ProductPlacement>().translationIndicator = hit.transform.gameObject.transform.GetChild(1).gameObject;
                        productPlacement.GetComponent<ProductPlacement>().rotationIndicator = hit.transform.gameObject.transform.GetChild(0).gameObject;
                        productPlacement.GetComponent<TouchHandler>().augmentationObject = hit.transform;
                        Debug.LogWarning(productPlacement.GetComponent<ProductPlacement>().chair.name);
                    }
                    else if(productPlacement.GetComponent<ProductPlacement>().chair != hit.transform.gameObject && delete == true)
                    {
                        Debug.LogWarning("Delete Test");
                        delete = false;
                        if (hit.transform.gameObject.name.Contains("(Clone)"))
                        {
                            Destroy(hit.transform.gameObject);
                        }
                        else
                        {
                            Debug.LogWarning("Parent Object");
                            hit.transform.position = new Vector3(1000, 1000, 1000);
                        }
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
               if(productPlacement.GetComponent<ProductPlacement>().chair != hit.transform.gameObject && delete == false)
                {
                    productPlacement.GetComponent<ProductPlacement>().chair = hit.transform.gameObject;
                    productPlacement.GetComponent<ProductPlacement>().translationIndicator = hit.transform.gameObject.transform.GetChild(1).gameObject;
                    productPlacement.GetComponent<ProductPlacement>().rotationIndicator = hit.transform.gameObject.transform.GetChild(0).gameObject;
                    productPlacement.GetComponent<TouchHandler>().augmentationObject = hit.transform;
                    Debug.LogWarning(productPlacement.GetComponent<ProductPlacement>().chair.name);
                }
                else if (delete == true)
                {
                    Debug.LogWarning("Delete Test");
                    delete = false;
                    if (hit.transform.gameObject.name.Contains("(Clone)"))
                    {
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("Parent Object");
                        hit.transform.position = new Vector3(1000, 1000, 1000);
                    }
                }

            }
        }
    }

    //Change the target of the product placement and touch handler script with an object from items array
    public void ChangeTarget(int i)
    {
        indicator.transform.localPosition = new Vector3(0, 0, 0);
        //if (indicator.GetComponent<MeshRenderer>().enabled)
        // {
        //currentItem.GetComponent<Rigidbody>().isKinematic = false;
           
            if (currentItem != null)
            {
                currentItem.transform.GetChild(0).gameObject.SetActive(false); //Set the indicators at the bottom as false
                currentItem.transform.GetChild(1).gameObject.SetActive(false);
            }

            if (productPlacement.GetComponent<ProductPlacement>().chair == null)
            {
                productPlacement.GetComponent<ProductPlacement>().chair = items[i];
                productPlacement.GetComponent<ProductPlacement>().translationIndicator = items[i].transform.GetChild(1).gameObject;
                productPlacement.GetComponent<ProductPlacement>().rotationIndicator = items[i].transform.GetChild(0).gameObject;
                productPlacement.GetComponent<TouchHandler>().augmentationObject = items[i].transform;
                items[i].SetActive(true);
                items[i].gameObject.transform.position = new Vector3(1000, 1000, 100);
                currentItem = items[i];
                productPlacement.GetComponent<ProductPlacement>().chair.GetComponent<MeshRenderer>().enabled = true;
                productPlacement.GetComponent<ProductPlacement>().chair.GetComponent<MeshCollider>().enabled = true;

            }
            GameObject copy;
            Debug.LogError("Same Item Selected");
            copy = Instantiate(productPlacement.GetComponent<ProductPlacement>().chair /*new Vector3(chair.transform.position.x, chair.transform.position.y, chair.transform.position.z)*/);
            copy.transform.position = productPlacement.GetComponent<ProductPlacement>().chair.transform.position;
            copy.transform.rotation = productPlacement.GetComponent<ProductPlacement>().chair.transform.rotation;
            productPlacement.GetComponent<ProductPlacement>().chair.transform.position = new Vector3(1000, 1000, 100);
            copy.transform.GetChild(0).gameObject.SetActive(false);
            copy.transform.GetChild(1).gameObject.SetActive(false);


            productPlacement.GetComponent<ProductPlacement>().chair = items[i];
            productPlacement.GetComponent<ProductPlacement>().translationIndicator = items[i].transform.GetChild(1).gameObject;
            productPlacement.GetComponent<ProductPlacement>().rotationIndicator = items[i].transform.GetChild(0).gameObject;
            productPlacement.GetComponent<TouchHandler>().augmentationObject = items[i].transform;
            productPlacement.GetComponent<ProductPlacement>().chair.GetComponent<MeshRenderer>().enabled = true;
            productPlacement.GetComponent<ProductPlacement>().chair.GetComponent<MeshCollider>().enabled = true;
            items[i].SetActive(true);
            items[i].gameObject.transform.position = new Vector3(1000, 1000, 100);
            currentItem = items[i];
       // }
        /*else
        {
            Debug.LogError("Reticle Not Active");
        }*/
       
        //dindicator.GetComponent<MeshRenderer>().gameObject.SetActive(true);
        //productPlacement.GetComponent<ProductPlacement>().IsPlaced = false;

    }
    public void SetDeleteItem()
    {
        if (multiObject == true)
        {
            delete = true;
            productPlacement.GetComponent<ProductPlacement>().chair = null;
            productPlacement.GetComponent<TouchHandler>().augmentationObject = null;
        }
        else
        {
            delete = true;
            productPlacement.GetComponent<ProductPlacement>().chair = null;
            productPlacement.GetComponent<TouchHandler>().augmentationObject = null;
            //productPlacement.GetComponent<ProductPlacement>().chair.transform.position = new Vector3(0, 1000, 0);
        }
    }
}
