using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Deactivate(GameObject thing)
    {
        thing.SetActive(false);
    }

    public void Activate(GameObject thing)
    {
        thing.SetActive(true);
    }
}
