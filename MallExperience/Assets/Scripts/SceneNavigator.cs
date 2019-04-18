using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    public VuforiaSetupManager vsm;
    public void LoadMainScene()
    {
        SceneManager.UnloadSceneAsync(vsm.dataSetName);
        SceneManager.LoadScene("mainmenu");
    }
}
