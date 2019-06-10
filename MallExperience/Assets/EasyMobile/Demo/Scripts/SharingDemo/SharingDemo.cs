using UnityEngine;
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
        public GameObject ButtonManagerScript;
        public DecodeByStaticPic decoder;

        void Awake()
        {
            ButtonManagerScript = GameObject.FindGameObjectWithTag("FunctionManager");
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
           // StartCoroutine(CRSaveScreenshot());
            NatShareU.NatShare.SaveToCameraRoll(ImgSave); //Using NatShare to Save directly to user camera roll
            NativeUI.Alert("NeoVu", "Photo Saved");
        }


        public void CaptureScreenshots()
        {
            StartCoroutine(CaptureScreenshot());
        }

        public void Decode()
        {
            StartCoroutine(DecodeScreenshot());
        }



        public void ShareScreenshot()
        {
            NatShareU.NatShare.Share(ImgSave);  //using NatShare Plug in to share with OS native UI
            /*if(NatShareU.NatShare.Share(ImgSave) == true)     Debug to check if sharing is available
            {
                NativeUI.Alert("imageShared","");
            }

            else
            {
                NativeUI.Alert("Image Not Share", "");
            }*/
        }

        public void ExitPhoto()  //Used to exit the photo preview mode
        {
            ButtonManagerScript.GetComponent<ButtonManager>().buttonsEnable();
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
            ButtonManagerScript.GetComponent<ButtonManager>().buttonsDisable();
            Debug.Log("Picture Taken");
            yield return new WaitForEndOfFrame();

            Texture2D texture = Sharing.CaptureScreenshot();
            Debug.Log("Texture Saved");
            targPare.SetActive(true);
            targImg.texture = texture;
            if(decoder != null)
            {
                decoder.targetTex = texture;
            }
            ImgSave = texture; //Display the taken photo on the users screen 
            
        }
        IEnumerator DecodeScreenshot()
        {
            yield return new WaitForEndOfFrame();

            Texture2D texture = Sharing.CaptureScreenshot();
            if (decoder != null)
            {
                decoder.targetTex = texture;
                decoder.Decode();
            }
            else
            {
                Debug.LogError("Decoder hasnt been assigned dummy");
            }
            ImgSave = texture; //Display the taken photo on the users screen 

        }
    }
}
    