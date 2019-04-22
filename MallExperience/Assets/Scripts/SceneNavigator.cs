using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public string sceneName;
    public AWSManager awsm;

    public void Start()
    {
        awsm = GameObject.Find("AWS_Manager").GetComponent<AWSManager>();
    }
    public void LoadMainScene()
    {
        Debug.Log("STEP 1");
        SceneManager.LoadScene("mainmenu");
        awsm.DeleteStreamingAssetsAndResetCheck();
        Debug.Log("STEP 2");
    }
}
