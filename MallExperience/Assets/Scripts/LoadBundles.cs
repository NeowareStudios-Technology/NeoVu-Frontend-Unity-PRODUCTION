using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadBundles : MonoBehaviour
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
        
            StartCoroutine(GetAssetBundle());
    }

    IEnumerator GetAssetBundle()
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
    
    public void Business1()
    {
        if (request.isDone)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business1");
            request.Send();

            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
            //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
            //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
            SceneManager.LoadScene("Business1");
                        
        }
    }

    public void Business2()
    {
        if (request.isDone)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business2");
            request.Send();

            if (request.isDone)
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
                //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
                SceneManager.LoadScene("Business2");
            }
        }
    }

    public void Business3()
    {
        if (request.isDone)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business3");
            request.Send();

            if (request.isDone)
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
                //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
                SceneManager.LoadScene("Business3");
            }
        }
    }

    public void Business4()
    {
        if (request.isDone)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business4");
            request.Send();

            if (request.isDone)
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
                //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
                SceneManager.LoadScene("Business4");
            }

        }
    }

    public void Business5()
    {
        if (request.isDone)
        {
            request = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/modelmaster/business5");
            request.Send();

            if (request.isDone)
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                //GameObject logo = bundle.LoadAsset<GameObject>("NWLogo");
                //Instantiate(logo, transform.position + transform.forward * -3, transform.rotation);
                SceneManager.LoadScene("Business5");
            }
        }
    }
}
