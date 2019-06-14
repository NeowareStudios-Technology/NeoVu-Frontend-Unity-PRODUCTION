using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTagger : MonoBehaviour
{
    public string myTag;
    // Start is called before the first frame update
    void Awake()
    {
        this.gameObject.tag = myTag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
