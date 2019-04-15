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

public class LoadBundleAndDataSet : MonoBehaviour
{
    private AssetBundleCreateRequest bundleRequest;
    private UnityWebRequest request;

    AmazonS3Client S3Client;
    public string bucketName;
    public string bundleName;
    public string S3Region = RegionEndpoint.USWest1.SystemName;
    public string saveFilePath;
    public string dataSetName;
    private int counter = 0;

    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }
    

    private void Start()
    {
        //needed for AWS sdk to work
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        // Initialize the Amazon Cognito credentials provider
        CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "us-east-1:ad940b00-0da0-42e9-b93d-5c58bc7ddee6", // Identity pool ID
            RegionEndpoint.USEast1 // Region
        );

        //initialize S3
        S3Client = new AmazonS3Client(credentials, _S3Region);
    }


    public void GetDataSetAndAssetBundleFromS3()
    {
        SaveObjectsInBucketLocally();        
        StartCoroutine(GetAssetBundle());
    }


    private void SaveObjectsInBucketLocally()
    {
        var request = new ListObjectsRequest()
        {
            BucketName = bucketName
        };

        //make requet to S3 to list all objects in bucket
        S3Client.ListObjectsAsync(request, (responseObject) =>
        {
            if (responseObject.Exception == null)
            {
                //for each object in the bucket
                responseObject.Response.S3Objects.ForEach((o) =>
                {
                    //pass the name of the object to function that gets it from S3 and 
                    //saves it locally
                    if((o.Key.Contains(".dat")) || (o.Key.Contains(".xml")))
                    {
                        SaveS3ObjectLocally(o.Key);
                    }

                });
            }
            else
            {
                Debug.Log("Error Listing Objects");
            }
        });
    }


    private IEnumerator GetAssetBundle()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle("https://s3-us-west-1.amazonaws.com/testvuforia/"+bundleName);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            SceneManager.LoadScene(bundleName);
        }
        
    }


    //get file from s3 bucket and save to streaming assets folder
    private void SaveS3ObjectLocally(String fileName)
    {
        S3Client.GetObjectAsync(bucketName, fileName, (responseObj) =>
        {
            var response = responseObj.Response;

            //if this object exists in the bucket...
            if (response.ResponseStream != null)
            {
                //set path to save file downloaded from s3 to
                saveFilePath = Application.streamingAssetsPath + "/Vuforia/" + fileName;

                //read file into buffer
                byte[] buffer = new byte[(int)response.ResponseStream.Length];
                response.ResponseStream.Read(buffer, 0, (int)response.ResponseStream.Length);
                
                //save buffer contents to local file
                Save(buffer, saveFilePath);
            }
        });
    }


    //write contents to new file
    private void Save(byte[] contents, string outputPath)
    {
        //DEBUG
        Debug.Log("SAVE S3 OBJECT: " + outputPath);

        File.WriteAllBytes(outputPath, contents);

        // Verify that the File has been actually stored
        if (File.Exists(outputPath))
        {
            Debug.Log("File successfully saved at: " + outputPath);
            //ActivateDatasetfromStreamingAssets();
        }
        else
        {
            Debug.Log("Failure!! - File does not exist at: " + outputPath);
        }
    }
}
