using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFlip : MonoBehaviour
{
   
    public int value;
    private Quaternion target;
    // Start is called before the first frame update
    void Start()
    {
        target = Quaternion.Euler(0, transform.rotation.y, transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {

                // Construct a ray from the current touch coordinates
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit hit;
                Debug.Log("RayCast Sent");
                // Create a particle if hit
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        this.transform.Rotate(180, 0, 180);
                        this.GetComponent<BoxCollider>().enabled = false;
                        this.GetComponent<MatchCard>().sendValue();
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("RayCast Sent");
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    this.transform.Rotate(180, 0, 180);
                    this.GetComponent<BoxCollider>().enabled = false;
                    this.GetComponent<MatchCard>().sendValue();
                }

            }
        }
    }
}
