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

public class MenuFrame : MonoBehaviour
{
    public string viewsBucketName = "neoware-neovu-views";
    private UnityWebRequest request;
    public AmazonS3Client S3Client;
    public VersionNumberJSON vnj;
    public string fold;
    public string fill;
    public TMPro.TextMeshProUGUI text;
    public TMPro.TextMeshProUGUI desc;
    public TMPro.TextMeshProUGUI address;
    public AWSManager awsm;
    public string S3Region = RegionEndpoint.USEast1.SystemName;
    public string metaBucketName = "neoware-neovu-meta";
    public ViewListJSON vlj;
    public MenuDataJSON mdj;
    public string[] largeCards;
    public string[] smallCards;
    public MenuLargeCardsJSON mlc;
    public GameObject[] largeCardObjs;
    public GameObject largeCardHolder;
    public GameObject smallcardObj;
    public GameObject smallCardHolder;

    // Start is called before the first frame update

    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private void Start()
    {
       
        //needed for AWS sdk to work
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        CognitoAWSCredentials credentials = new CognitoAWSCredentials(
            "us-east-1:ad940b00-0da0-42e9-b93d-5c58bc7ddee6", // Identity pool ID
            RegionEndpoint.USEast1 // Region
        );

        //initialize S3
        S3Client = new AmazonS3Client(credentials, _S3Region);

        //this is needed for search functionality
        CreateLargeCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MenuRead()
    {

    }
    public void CreateLargeCards()
    {
        float largeCardx = largeCardHolder.transform.position.x;
        string MenuObjectPath = "mainmenu/largecards.json";
        S3Client.GetObjectAsync(viewsBucketName, MenuObjectPath, (responseObj) =>
        {
        string JsonData = null;
        var response = responseObj.Response;
        Debug.Log(response);
        if (response.ResponseStream != null)
        {
            using (StreamReader reader = new StreamReader(response.ResponseStream))
            {
                JsonData = reader.ReadToEnd();
                Debug.Log("found s3 object");
            }
        }
        else
        {
            Debug.Log("Could not find S3 object");
        }
        Debug.Log(JsonData);
        mlc = JsonUtility.FromJson<MenuLargeCardsJSON>(JsonData);
        largeCards = mlc.cards.ToString().Split(',');

        //mdj = JsonUtility.FromJson<MenuDataJSON>(JsonData);
        //Debug.LogWarning(mdj.description);
        //save downloaded JSON as a class in unity
        //text.text = mdj.title;
        //desc.text = mdj.description;
        //address.text = mdj.address;
        for (int i = 0; i < largeCards.Length; i++)
        {
            largeCardx = largeCardx + 150;
            Debug.LogError(i);
                GameObject currObject = largeCardObjs[i];
            Debug.LogWarning("Building");
            Debug.LogError(largeCards[i]);
                string S3ObjectPath = largeCards[i] + "/" + largeCards[i]+"data.json";
                Debug.Log(S3ObjectPath);
                S3Client.GetObjectAsync(viewsBucketName, S3ObjectPath, (responseObjs) =>
                {
                    string VuData = null;
                    var responses = responseObjs.Response;
                    Debug.Log(responses);
                    if (responses.ResponseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responses.ResponseStream))
                        {
                            VuData = reader.ReadToEnd();
                            Debug.Log("found s3 object");
                        }
                    }
                    else
                    {
                        Debug.Log("Could not find S3 object");
                    }
                    Debug.Log(VuData);
                    mdj = JsonUtility.FromJson<MenuDataJSON>(VuData);
                    Debug.LogWarning(mdj.description);
                    //save downloaded JSON as a class in unity
                    currObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.title;
                    currObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.description;
                    currObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.address;
                });
                // currObj.transform.parent = largeCardHolder.transform;
            }
        });





    }

        public void createSmallCards()
        {
        float largeCardx = largeCardHolder.transform.position.x;
    string MenuObjectPath = "mainmenu/smallcards.json";
    S3Client.GetObjectAsync(viewsBucketName, MenuObjectPath, (responseObj) =>
        {
        string JsonData = null;
    var response = responseObj.Response;
    Debug.Log(response);
        if (response.ResponseStream != null)
        {
            using (StreamReader reader = new StreamReader(response.ResponseStream))
            {
                JsonData = reader.ReadToEnd();
                Debug.Log("found s3 object");
            }
        }
        else
        {
            Debug.Log("Could not find S3 object");
        }
        Debug.Log(JsonData);
        mlc = JsonUtility.FromJson<MenuLargeCardsJSON>(JsonData);
        largeCards = mlc.cards.ToString().Split(',');

        //mdj = JsonUtility.FromJson<MenuDataJSON>(JsonData);
        //Debug.LogWarning(mdj.description);
        //save downloaded JSON as a class in unity
        //text.text = mdj.title;
        //desc.text = mdj.description;
        //address.text = mdj.address;
        for (int i = 0; i<largeCards.Length; i++)
        {
            largeCardx = largeCardx + 150;
            Debug.LogError(i);
            GameObject currObject = Instantiate(smallcardObj, largeCardHolder.transform);
currObject.transform.position = new Vector3(largeCardx, currObject.transform.position.y, currObject.transform.position.z);
Debug.LogWarning("Building");
            Debug.LogError(largeCards[i]);
                string S3ObjectPath = largeCards[i] + "/" + largeCards[i] + "data.json";
Debug.Log(S3ObjectPath);
                S3Client.GetObjectAsync(viewsBucketName, S3ObjectPath, (responseObjs) =>
                {
                    string VuData = null;
var responses = responseObjs.Response;
Debug.Log(responses);
                    if (responses.ResponseStream != null)
                    {
                        using (StreamReader reader = new StreamReader(responses.ResponseStream))
                        {
                            VuData = reader.ReadToEnd();
                            Debug.Log("found s3 object");
                        }
                    }
                    else
                    {
                        Debug.Log("Could not find S3 object");
                    }
                    Debug.Log(VuData);
                    mdj = JsonUtility.FromJson<MenuDataJSON>(VuData);
                    Debug.LogWarning(mdj.description);
                    //save downloaded JSON as a class in unity
                    currObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.title;
                    currObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.description;
                    currObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.address;
                });
                // currObj.transform.parent = largeCardHolder.transform;
            }
        });





    }


}
