using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketConnector : MonoBehaviour
{
    public BoxCollider boxy;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogError("Start Called" + this.transform.name);
        boxy = this.GetComponent<BoxCollider>();
        this.GetComponent<BoxCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("Collision");
        /*if (other.tag == "Socket")
        {
            this.transform.position = other.transform.position;
        }*/
    }
}
