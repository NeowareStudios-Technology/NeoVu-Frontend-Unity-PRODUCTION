using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHolder : MonoBehaviour
{
    public string firstName;
    public string lastName;
    public string email;
    public int age;
    public string gender;
    // Start is called before the first frame update
    void Start()
    {
        firstName   =     PlayerPrefs.GetString("First Name");
        lastName    =     PlayerPrefs.GetString("Last Name");
        email       =     PlayerPrefs.GetString("Email");
        age         =     PlayerPrefs.GetInt("Age");
        gender      =     PlayerPrefs.GetString("Gender");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
