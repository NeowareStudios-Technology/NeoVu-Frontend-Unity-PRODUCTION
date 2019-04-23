﻿/************************************
 * Project: NeoWare Lens
 * Author: David Lee Ramirez
 * Date: 4/16/19
 * Copywrite NeoWare 2019
 * *********************************/

using System.Collections;
using System.Collections.Generic;
using Vuforia;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using Amazon.CognitoIdentity;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.S3.Util;
using System;
using System.IO;

public class AWSManager : MonoBehaviour
{
    private AssetBundleCreateRequest bundleRequest;
    private UnityWebRequest request;
    public AmazonS3Client S3Client;
    public GameObject messageText;
    public InputField searchField;
    public Text bucketListText;
    public string nameOfView;
    public string S3Region = RegionEndpoint.USEast1.SystemName;
    public string dataSetPath;
    public string neoBucketName = "neoware-neovu";
    //need to download .xml and .dat file (ie. 2 files)
    private int numFilesToDownload = 2;
    public int filesDownloaded = 0;
    //set this to false when you want to load a new lens from the main scene
    public bool getAssetBundleCheck = true;

    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    

    private void Start()
    {
        //needed for AWS sdk to work
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        //This is for Danny account
        CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "us-east-1:ad940b00-0da0-42e9-b93d-5c58bc7ddee6", // Identity pool ID
            RegionEndpoint.USEast1 // Region
        );

       /*  
        //this is for the NeoWare account
        CognitoAWSCredentials credentials = new CognitoAWSCredentials (
            "us-east-2:6e630d64-0f8f-45ab-9f52-76db64e254ed", // Identity pool ID
            RegionEndpoint.USEast2 // Region
        );
*/
        //initialize S3
        S3Client = new AmazonS3Client(credentials, _S3Region);
    }


    //get the asset bundle for the scene and load the scene ONLY AFTER downloading dataset files
    void Update()
    {
        if ((getAssetBundleCheck == true) && (filesDownloaded == numFilesToDownload))
        {
            StartCoroutine(GetAssetBundle(nameOfView));
            filesDownloaded = 0;
            getAssetBundleCheck = false;
        }
    } 


    private IEnumerator GetAssetBundle(string paramNameOfView)
    {
        //get different asset bundles depending on platform
        #if UNITY_IOS
		    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3.amazonaws.com/"+ neoBucketName+"/"+paramNameOfView +"/"+paramNameOfView+".ios");
		#elif UNITY_ANDROID
		    UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3.amazonaws.com/"+ neoBucketName+"/"+paramNameOfView +"/"+paramNameOfView+".and");
		#else
		   UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3.amazonaws.com/"+ neoBucketName+"/"+paramNameOfView +"/"+paramNameOfView);
        #endif

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene(nameOfView);
        }  
    }


    //download Vuforia DataSet seperately from asset bundle because it cant be packaged in the asset bundle
    public void DowloadDataSet(string nameOfSelectedView)
    {
        Debug.Log("starting download");
        nameOfView = nameOfSelectedView;

        string XMLFileName = nameOfView + ".xml";
        string DATFileName = nameOfView + ".dat";
        SaveS3ObjectLocally(nameOfView,XMLFileName);
        SaveS3ObjectLocally(nameOfView,DATFileName);
    }


    //get file from s3 bucket and save to streaming assets folder
    private void SaveS3ObjectLocally(String directoryName, String fileName)
    {
        //the directory containing the Vu will always have th same name as the dataset file names
        string S3ObjectPath= directoryName + "/" + fileName;
        Debug.Log(S3ObjectPath);
        S3Client.GetObjectAsync(neoBucketName, S3ObjectPath, (responseObj) =>
        {
            var response = responseObj.Response;

            //if this object exists in the bucket...
            if (response.ResponseStream != null)
            {
                //set path to save file downloaded from s3 to
                dataSetPath = Path.Combine(Application.persistentDataPath,fileName);

                //read file into buffer
                byte[] buffer = new byte[(int)response.ResponseStream.Length];
                response.ResponseStream.Read(buffer, 0, (int)response.ResponseStream.Length);
                
                //save buffer contents to local file
                SaveToFile(buffer, dataSetPath);
            }
            else
            {
                Debug.Log("Could not find S3 object");
            }
        });
}

    //write contents to new file
    private void SaveToFile(byte[] contents, string outputPath)
    {
        //DEBUG
        Debug.Log("SAVE S3 OBJECT: " + outputPath);

        File.WriteAllBytes(outputPath, contents);

        // Verify that the File has been actually stored
        if (File.Exists(outputPath))
        {
            Debug.Log("File successfully saved at: " + outputPath);
            filesDownloaded++;
        }
        else
        {
            Debug.Log("Failure!! - File does not exist at: " + outputPath);
        }
    }


    //call this when returning to main scene from any downloaded lens
    public void DeleteStreamingAssetsAndResetCheck()
    {
        System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        
        //delete all downloaded streaming assets (ie. vuforia datasets)
        foreach (FileInfo file in di.GetFiles())
        {
            file.Delete(); 
        }

        //set to true so asset bundles can be downloaded only after downloading dataset
        getAssetBundleCheck = true;
    }

}