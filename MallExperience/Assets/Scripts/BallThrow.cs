using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour

{
    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    [SerializeField]
    float throwForceInXamdY = 1f;

    [SerializeField]
    float throwForceInZ = 100f;

    Rigidbody rb;

    public GameObject par;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            startPos = Input.GetTouch(0).position;
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeFinish = Time.time;

            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.GetTouch(0).position;
            direction = startPos - endPos;
            rb.isKinematic = false;
            rb.AddForce(-direction.x * throwForceInXamdY, -direction.y * throwForceInXamdY, throwForceInZ / timeInterval);

            Destroy(gameObject, 7f);

        }

        if (Input.GetMouseButtonDown(0))
        {
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchTimeFinish = Time.time;

            timeInterval = touchTimeFinish - touchTimeStart;

            endPos = Input.mousePosition;
            direction = startPos - endPos;
            rb.isKinematic = false;
            rb.AddForce(-direction.x * throwForceInXamdY, -direction.y * throwForceInXamdY, throwForceInZ / timeInterval);
            this.transform.parent = par.transform;

            Destroy(gameObject, 7f);
        }
    }
}
