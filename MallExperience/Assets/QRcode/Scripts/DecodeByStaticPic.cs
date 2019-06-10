using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DecodeByStaticPic : MonoBehaviour {

	public Texture2D targetTex;
	public TMPro.TextMeshProUGUI resultText;
    public GameObject QRTarget;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Decode()
	{
		string resultStr =QRCodeDecodeController.DecodeByStaticPic (targetTex);
        Debug.Log(resultStr);
		resultText.text = resultStr;
	}
}
