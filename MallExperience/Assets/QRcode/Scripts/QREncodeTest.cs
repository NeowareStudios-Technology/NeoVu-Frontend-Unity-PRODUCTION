﻿/// <summary>
/// write by 52cwalk,if you have some question ,please contract lycwalk@gmail.com
/// </summary>
/// 
/// 

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QREncodeTest : MonoBehaviour {
	public QRCodeEncodeController e_qrController;
	public RawImage qrCodeImage;
    public string CodeText;
    public TMPro.TextMeshProUGUI Info;
    public GameObject EncodeScreen;

    public Texture2D codeTex;
	// Use this for initialization
	void Start () {
		if (e_qrController != null) {
			e_qrController.onQREncodeFinished += qrEncodeFinished;//Add Finished Event
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void qrEncodeFinished(Texture2D tex)
	{
		if (tex != null && tex != null) {
			int width = tex.width;
			int height = tex.height;
			float aspect = width * 1.0f / height;
			//qrCodeImage.GetComponent<RectTransform> ().sizeDelta = new Vector2 (170, 170.0f / aspect);
			qrCodeImage.texture = tex;
            codeTex = tex;
        } else {
		}
	}

    public void setCodeType(int typeId)
    {
        e_qrController.eCodeFormat = (QRCodeEncodeController.CodeMode)(typeId);
        Debug.Log("clicked typeid is " + e_qrController.eCodeFormat);
    }


    public void Encode()
	{
		if (e_qrController != null) {
            string valueStr = CodeText;
			int errorlog = e_qrController.Encode(valueStr);
            EncodeScreen.SetActive(true);
            Info.color = Color.red;
			if (errorlog == -13) {
				Info.text = "Must contain 12 digits,the 13th digit is automatically added !";

			} else if (errorlog == -8) {
                Info.text = "Must contain 7 digits,the 8th digit is automatically added !";
			}
            else if (errorlog == -39)
            {
                Info.text = "Only support digits";
            }
            else if (errorlog == -128) {
                Info.text = "Contents length should be between 1 and 80 characters !";

			} else if (errorlog == -1) {
                Info.text = "Please select one code type !";
			}
			else if (errorlog == 0) {
                Info.color = Color.green;
                Info.text = "Encode successfully !";
			}
		}
	}

	public void ClearCode()
	{
		qrCodeImage.texture = null;
		CodeText = "";
        Info.text = "";
        Info.text = "";
	}
    
    public void SaveCode()
    {
        if (codeTex != null)
        {
            GalleryController.SaveImageToGallery(codeTex);
        }

    }
}
