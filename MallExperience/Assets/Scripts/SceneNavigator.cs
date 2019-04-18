using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public string sceneName;
    public void LoadMainScene()
    {
        Debug.Log("STEP 1");
        SceneManager.UnloadSceneAsync(sceneName);
        Debug.Log("STEP 2");
        SceneManager.LoadScene("mainmenu");
        Debug.Log("STEP 3");
    }
}
