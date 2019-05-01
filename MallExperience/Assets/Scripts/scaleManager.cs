using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scaleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(this.transform.localScale);
        if (this.transform.localScale.x < 1)
        {
            transform.localScale += new Vector3(1.2f, 1.2f, 1f);
            Debug.Log("trying to force scale");
        }
    }
}
