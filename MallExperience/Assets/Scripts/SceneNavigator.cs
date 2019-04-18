using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public string sceneName;
    public void LoadMainScene()
    {
        SceneManager.UnloadSceneAsync(sceneName);
        SceneManager.LoadScene("mainmenu");
    }
}
