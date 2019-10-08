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
    public MenuSmallDataJSON msj;
    public string[] largeCards;
    public string[] smallCards;
    public MenuLargeCardsJSON mlc;
    public MenuSmallCardsJSON msc;
    public GameObject[] largeCardObjs;
    public GameObject largeCardHolder;
    public GameObject[] smallcardObjs;
    public GameObject smallCardHolder;
    public Sprite test;
    public string tester;
    public GameObject bigCard;

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
        createSmallCards();
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

        for (int i = 0; i < largeCards.Length; i++)
        {
            
            Debug.LogError(i);
            GameObject currObject = largeCardObjs[i];
            Debug.LogError(largeCards[i]);
                string S3ObjectPath = largeCards[i] + "/" + largeCards[i]+"carddata.json";
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
                    Debug.LogWarning(mdj.shortdescription);
                    //save downloaded JSON as a class in unity
                    currObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.title;
                    currObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.shortdescription;
                    currObject.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.category;
                });
                RawImage currImage = currObject.transform.GetChild(3).GetComponent<RawImage>();
                string S3ImagePath = largeCards[i] + "/" + largeCards[i] + "largeimage.png";
                Debug.LogError(S3ImagePath);

                StartCoroutine(loadSpriteImageFromUrl(S3ImagePath, currImage));
                // currObj.transform.parent = largeCardHolder.transform;
            }
        });





    }

    public void createSmallCards()
    {
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
            Debug.LogError(JsonData);
            msc = JsonUtility.FromJson<MenuSmallCardsJSON>(JsonData);
            smallCards = msc.cards.ToString().Split(',');

            for (int i = 0; i< smallCards.Length; i++)
            {
                
                Debug.LogError(i);
                GameObject currObject = smallcardObjs[i];
                Debug.LogWarning("Building");
                Debug.LogError(smallCards[i]);
                string S3ObjectPath = smallCards[i] + "/" + smallCards[i] + "data.json";
                Debug.Log(S3ObjectPath);
                    S3Client.GetObjectAsync(viewsBucketName, S3ObjectPath, (responseObjs) =>
                    {
                        Debug.LogWarning("Test");
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
                        Debug.LogWarning("mdj Set");
                        Debug.LogWarning(msj.description);
                        //save downloaded JSON as a class in unity
                        currObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.title;
                        
                        Debug.LogError(msj.title.Length);
                        currObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.category;
                        tester = msj.title;
                    });
                    // currObj.transform.parent = largeCardHolder.transform;
                }
        });





    }

    IEnumerator loadSpriteImageFromUrl(string URL, RawImage display)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("https://neoware-neovu-views.s3.amazonaws.com/" + URL))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.LogError(uwr.error);
            }
            else
            {
                Debug.LogError("Success");
                // Get downloaded asset bundle
                var texture = DownloadHandlerTexture.GetContent(uwr);
                display.texture = texture;
                display.color = new Color(255, 255, 255);
            }
        }
    }

    public void generateBigCard(string cardName)
    {
        bigCard.SetActive(true);

        string S3ObjectPath = cardName + "/" + cardName + "carddata.json";
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
            Debug.LogWarning(mdj.shortdescription);
            //save downloaded JSON as a class in unity
            bigCard.transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.title;
            bigCard.transform.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.summary;
            bigCard.transform.GetChild(5).GetComponent<TMPro.TextMeshProUGUI>().text = mdj.address;
            bigCard.transform.GetChild(7).transform.name = mdj.title.ToLower();
        });
        RawImage currImage = bigCard.transform.GetChild(0).GetComponent<RawImage>();
        string S3ImagePath = cardName + "/" + cardName + "cardimage.png";
        Debug.LogError(S3ImagePath);

        StartCoroutine(loadSpriteImageFromUrl(S3ImagePath, currImage));
        // currObj.transform.parent = largeCardHolder.transform;
    }

}



