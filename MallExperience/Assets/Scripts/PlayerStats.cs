using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public string gender;
    public float age;
    // Start is called before the first frame update
    void Start()
    {
        age = PlayerPrefs.GetFloat("Age");
        gender = PlayerPrefs.GetString("Gender");
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void resetPrefs()
    {
        age = PlayerPrefs.GetFloat("Age");
        gender = PlayerPrefs.GetString("Gender");
    }
}
