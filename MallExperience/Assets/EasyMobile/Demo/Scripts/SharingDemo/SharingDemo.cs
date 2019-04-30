﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using EasyMobile;

namespace EasyMobile.Demo
{
    public class SharingDemo : MonoBehaviour
    {
        // public Image clockRect;
        // public Text clockText;

        // Screenshot names don't need to include the extension (e.g. ".png")
        string TwoStepScreenshotName = "EM_Screenshot";
        string OneStepScreenshotName = "EM_OneStepScreenshot";
        string TwoStepScreenshotPath;
        string sampleMessage = "This is a sample sharing message #sampleshare";
        string sampleText = "Hello from Easy Mobile!";
        string sampleURL = "http://u3d.as/Dd2";
        public RawImage targImg;
        public GameObject targPare;
        public Texture2D ImgSave;

        void Awake()
        {
            // Init EM runtime if needed (useful in case only this scene is built).
            if (!RuntimeManager.IsInitialized())
                RuntimeManager.Init();
        }

        // void OnEnable()
        // {
        //     ColorChooser.colorSelected += ColorChooser_colorSelected;
        // }

        // void OnDisable()
        // {
        //     ColorChooser.colorSelected -= ColorChooser_colorSelected;
        // }

        // void ColorChooser_colorSelected(Color obj)
        // {
        //     clockRect.color = obj;
        // }

        // void Update()
        // {
        //     clockText.text = System.DateTime.Now.ToString("hh:mm:ss");
        // }

        public void ShareText()
        {
            Sharing.ShareText(sampleText);
        }

        public void ShareURL()
        {
            Sharing.ShareURL(sampleURL);
        }

        public void SaveScreenshot()
        {
            StartCoroutine(CRSaveScreenshot());
            NatShareU.NatShare.SaveToCameraRoll(ImgSave);
        }


        public void CaptureScreenshots()
        {
            StartCoroutine(CaptureScreenshot());
        }




        public void ShareScreenshot()
        {
            if (!string.IsNullOrEmpty(TwoStepScreenshotPath))
            {
                Sharing.ShareImage(TwoStepScreenshotPath, sampleMessage);
            }
            else
            {
                NativeUI.Alert("Alert", "Please save a screenshot first.");
            }
        }

        public void ExitPhoto()
        {
            targPare.SetActive(false);
        }

        public void OneStepSharing()
        {
            StartCoroutine(CROneStepSharing());
        }

        IEnumerator CRSaveScreenshot()
        {
            yield return new WaitForEndOfFrame();

            TwoStepScreenshotPath = Sharing.SaveScreenshot(TwoStepScreenshotName);

            NativeUI.Alert("Alert", "A new screenshot was saved at " + TwoStepScreenshotPath);
        }



        IEnumerator CROneStepSharing()
        {
            yield return new WaitForEndOfFrame();

            Sharing.ShareScreenshot(OneStepScreenshotName, sampleMessage);
        }


        IEnumerator CaptureScreenshot()
        {
            yield return new WaitForEndOfFrame();

            Texture2D texture = Sharing.CaptureScreenshot();
            Debug.Log("Texture Saved");
            targPare.SetActive(true);
            targImg.texture = texture;
            ImgSave = texture;
            
        }
    }
}
    