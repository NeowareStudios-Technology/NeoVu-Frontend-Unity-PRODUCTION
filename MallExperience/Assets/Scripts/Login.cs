using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;

public class Login : MonoBehaviour
{

    public TMPro.TMP_InputField userEmail;
    public TMPro.TMP_InputField userPassword;
    public TMPro.TMP_InputField loginEmail;
    public TMPro.TMP_InputField loginPassword;
    public TMPro.TextMeshProUGUI SignUpWarning;
    public GameObject signUpScreen;
    public TMPro.TextMeshProUGUI loginWarning;
    public GameObject verifyButton;
    private FirebaseUser currUser;
    private System.DateTime newDate; //Date Cutoff for 13 years
    public TMPro.TMP_InputField birthDate;
    public TMPro.TMP_Dropdown gender;
    public TMPro.TMP_InputField phoneNum;
    public string genderVal;
    System.DateTime birthCheck;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(System.DateTime.Today);
        System.DateTime newDate = System.DateTime.Today.AddYears(-13); //Set the date for 13 or older
        Debug.Log(newDate);
        Debug.Log((System.DateTime.Today - newDate).Days);
       // Debug.Log(gender.value);
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(gender.value);
    }

    //Pull up sign in screen
    public void SignUpPressed()
    {
        signUpScreen.SetActive(true);
        loginWarning.gameObject.SetActive(false);
    }
    public void resendVerify()
    {
        currUser.SendEmailVerificationAsync();
        loginWarning.text = ("Verification Email sent!");
    }


    public void PasswordCheck()
    {
        bool hasUpper = false;
        bool hasLower = false;
        bool hasNumber = false;
        var password = userPassword.text;
        var email = userEmail.text;
        
        //iterates through password to check for an uppercase letter, a lowercase one, and a number
        if (password.Length >= 8)
        {
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsLower(password[i]))
                {
                    hasLower = true;
                }
                else if (char.IsUpper(password[i]))
                {
                    hasUpper = true;
                }
                else if (char.IsNumber(password[i]))
                {
                    hasNumber = true;
                }
            }

            if (hasUpper == true && hasLower == true && hasNumber == true)
            {
                //if password passes check for neccessities of email adress
                if (userEmail.text.Contains("@") && userEmail.text.Contains("."))
                {
                     birthCheck = System.Convert.ToDateTime(birthDate.text);
                    if ((System.DateTime.Today - birthCheck).Days < 4748) //check to makes sure the age is over 13 years old, in days
                    {
                        SignUpWarning.text = "Age to low to use this application";
                    }
                    else
                    {
                        bool numCheck = true;
                        if (phoneNum.text == "")
                        {
                            SignUpWarning.text = "Input your phone number";
                        }
                        else if((phoneNum.text.Length > 10) || (phoneNum.text.Length < 9)) //check the phone number length to make sure it is legit
                        {
                            SignUpWarning.text = "This is an invalid phone number";
                        } 
                        else
                        {
                            for (int i = 0; i < phoneNum.text.Length; i++) //iterate through the phone number to make sure it is only numeric
                            {
                                if (char.IsLetter(phoneNum.text[i]))
                                {
                                    SignUpWarning.text = "The phone number entered is invalid";
                                    numCheck = false;
                                }
                            }
                            if (numCheck == true)
                            {
                                switch (gender.value)  //assign a gender from the drop down
                                {
                                    case 0:
                                        genderVal = "unspecified";
                                        break;
                                    case 1:
                                        genderVal = "female";
                                        break;
                                    case 2:
                                        genderVal = "male";
                                        break;

                                }
                                if (gender.value == 4)  //Throw an error if a gender is not picked
                                {
                                    SignUpWarning.tag = "Please pick a gender";
                                }
                                else
                                {
                                    SignUp();
                                    signUpScreen.SetActive(false);
                                    loginWarning.gameObject.SetActive(true);
                                    loginWarning.text = ("Sign Up Completed");
                                }
                            }
                        }
                    }
                }
                else
                {
                    SignUpWarning.text = "Valid Email Required";
                }
                
            }
            //feedback on loop failure
            else if (hasUpper == false)
            {
                SignUpWarning.text = "Make sure your password contains an uppercase letter";
            }
            else if (hasLower == false)
            {
                SignUpWarning.text = "Make sure your password contains a lowercase letter";
            }
            else if (hasNumber == false)
            {
                SignUpWarning.text = "Make sure your password contains a number";
            }
        }
        //feedback if password is too short
        else
        {
            SignUpWarning.text = "Your password must be atleast 8 characters long";
        }
    }
    public void setText()
    {
        if (birthDate.text == "")
        {
            birthDate.text = "mm/dd/yyyy";
        }
    }

    //Firebase Sign in Function
    public void SignUp()
    {
        var email = userEmail.text;
        var password = userPassword.text;

            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                newUser.SendEmailVerificationAsync();
                currUser = newUser;
                PlayerPrefs.SetFloat("Age", Mathf.Round(((System.DateTime.Today - birthCheck).Days) / 365));
                PlayerPrefs.SetString("Gender", genderVal);
                
            });
    }

    //Firebase Login Function
    public void LogIn()
    {
        var email = loginEmail.text;
        var password = loginPassword.text;
        

        
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                loginWarning.text = "Invalid username or password";
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            currUser = newUser;
            PlayerPrefs.SetString("Status", "Signed In");
            if (newUser.IsEmailVerified)
            {
                
                Debug.Log("Email Verified");
                this.GetComponent<AWSManager>().DowloadDataSet("mainmenu");
            }

            else
            {
                Debug.Log("Email Not Verified");
                loginWarning.text = "Please verify your email before logging in";
                verifyButton.SetActive(true);
            }
        });
    }
}
