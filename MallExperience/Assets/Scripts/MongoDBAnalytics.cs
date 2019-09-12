using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using MongoDB.Driver;

public class MongoDBAnalytics : MonoBehaviour
{
    private const string MONGO_URI = "mongodb://neovu:neoware1>@ds261817.mlab.com:61817/neovu";
    private const string DATABASE_NAME = "neovu";
    public MongoClient client;
    public IMongoDatabase db;

    public class User
    {

        public string VuName = "Alfonso";
    }

    // Start is called before the first frame update
    void Start()
    {
        TestData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestData()
    {

        client = new MongoClient("mongodb://neovu:neoware1@ds261817.mlab.com:61817/neovu");
        db = client.GetDatabase(DATABASE_NAME);
        IMongoCollection<User> userCollection = db.GetCollection<User>("users");
        userCollection.InsertOne(new User {});
    }

    public IEnumerator DatabaseCall() { 

        using (UnityWebRequest www = UnityWebRequest.Post("https://api.mlab.com/api/1/databases/neovu/collections/scaleAdjust?apiKey=LOI2GD7Kz3nS2VJF7OAwfAwVf-IXgOTN", "Test"))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                Debug.LogError("Form upload complete!");
            }

        }
    }
}

