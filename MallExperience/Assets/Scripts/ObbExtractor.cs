using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using System.IO;
using System;
using Amazon.S3.Util;
using System.Collections.Generic;
using Amazon.CognitoIdentity;
using Amazon;
using System.Text;
using Vuforia;
using System.Linq;




public class ObbExtractor : MonoBehaviour
{
    //How to use the script
    //- Attach this script to the game object in the scene that starts the build but prior to the AR initialization.
    //- The point of the script is to initialize the vuforia database when a split binary android app is created.
    //- This is due to the fact that when a build exceeds 100mb it requires a split binary application build
    //- This then seperates the content from the database

    AmazonS3Client S3Client;
    public string bucketName = "testvuforia";
    public string S3Region = RegionEndpoint.USWest1.SystemName;
    public string saveFilePath;
    public string dataSetName;
    private int counter = 0;


    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }


    void Awake()
    {

        Debug.Log(saveFilePath);

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

        //List objects in a bucket
        SaveObjectsInBucketLocally();

        StartCoroutine("ActivateDatasetfromStreamingAssets");
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
                    SaveS3ObjectLocally(o.Key);
                });
            }
            else
            {
                Debug.Log("Error Listing Objects");
            }
        });
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
                byte[] buffer = new byte[81920];
                response.ResponseStream.Read(buffer, 0, buffer.Length);

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


    private IEnumerator ActivateDatasetfromStreamingAssets()
    {
        yield return new WaitForSeconds(6);

        //DEBUG
        Debug.Log("Starting activate");

        ObjectTracker objectTracker = TrackerManager.Instance.GetTracker<ObjectTracker>();

        objectTracker.Stop();
		IEnumerable<DataSet> dataSetList = objectTracker.GetActiveDataSets();
  
		foreach(DataSet set in dataSetList.ToList()){
			objectTracker.DeactivateDataSet(set);
		}

		// Check if the data set exists at the given path.
		if (!DataSet.Exists(dataSetName))
		{
			Debug.LogError("Data set does not exist.");
        }

        // Create a new empty data set.
		DataSet dataSet = objectTracker.CreateDataSet();
	
		// Load the data set from the given path.
		if (!dataSet.Load(dataSetName))
		{
			Debug.LogError("Failed to load data set ");
		}
        else
        {
            Debug.Log("Dataset loaded successfully");
        }
	
		// (Optional) Activate the data set.
		objectTracker.ActivateDataSet(dataSet);
		objectTracker.Start();

        //AttachContentToTrackables(dataSet);
    }

}