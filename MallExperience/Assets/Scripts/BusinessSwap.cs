using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BusinessSwap : MonoBehaviour
{
    private AssetBundleCreateRequest bundleRequest;
    private UnityWebRequest request;

    private void Start()
    {
        /*request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/mainscene");
        request.Send();

        if (request.isDone)
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
            //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
            SceneManager.LoadScene("LoadScene");
        }*/
    }
    public void CallBusiness1()
    {
        StartCoroutine(Business1());
    }

    public void CallBusiness2()
    {
        StartCoroutine(Business2());
    }

    public void CallBusiness3()
    {
        StartCoroutine(Business3());
    }

    public void CallBusiness4()
    {
        StartCoroutine(Business4());
    }

    public void CallBusiness5()
    {
        StartCoroutine(Business5());
    }
    public void CallBack()
    {
        StartCoroutine(Back());
    }

    IEnumerator Business1()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business1");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("Business1");
        }

    }

    IEnumerator Business2()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business2");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("Business2");
        }

    }

    IEnumerator Business3()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business3");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("Business3");
        }

    }

    IEnumerator Business4()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business4");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("Business4");
        }

    }

    IEnumerator Business5()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business5");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("Business5");
        }

    }

    IEnumerator Back()
    {

        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/mainscene");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene("LoadScene");
        }

    }
}