using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase;
using Firebase.Analytics;

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
    public GameObject loginScreen;
    public TMPro.TMP_InputField firstName;
    public TMPro.TMP_InputField lastName;
    public int ageAnalytics;
    public string firstNameAnalytics;
    public string lastNameAnalytics;
    public string emailAnalytics;
    public string genderAnalytics;
    public int age;
    // Start is called before the first frame update
    void Start()
    {
        
        FirebaseAuth.DefaultInstance.ToString();
        Debug.Log(System.DateTime.Today);
        System.DateTime newDate = System.DateTime.Today.AddYears(-13); //Set the date for 13 or older
        Debug.Log(newDate);
        Debug.Log((System.DateTime.Today - newDate).Days);
        if (PlayerPrefs.GetString("Status") == "Signed In")
        {
            loginScreen.SetActive(true);
            LogIn();
        }
        Debug.LogError(PlayerPrefs.GetString("Status"));

        emailAnalytics = (PlayerPrefs.GetString("Email"));
        firstNameAnalytics = (PlayerPrefs.GetString("First Name"));
        lastNameAnalytics = (PlayerPrefs.GetString("Last Name"));
        genderAnalytics = (PlayerPrefs.GetString("Gender"));
        ageAnalytics = (PlayerPrefs.GetInt("Age"));

        Debug.LogWarning(emailAnalytics);
        Debug.LogWarning(firstNameAnalytics);
        Debug.LogWarning(lastNameAnalytics);
        Debug.LogWarning(genderAnalytics);
        Debug.LogWarning(ageAnalytics);
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

    public void CloseSignup()
    {
        signUpScreen.SetActive(false);
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
                    System.DateTime temp;
                    if (System.DateTime.TryParse(birthDate.text, out temp))
                    {


                        birthCheck = System.DateTime.Parse(birthDate.text);
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
                            else if ((phoneNum.text.Length > 10) || (phoneNum.text.Length < 9)) //check the phone number length to make sure it is legit
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
                                        if( firstName.text == "First Name" || firstName.text == null)
                                        {
                                            loginWarning.text = "Please input your first name";
                                        }
                                        else
                                        {
                                            if (lastName.text == "Last Name" || lastName.text == null)
                                            {
                                                loginWarning.text = "Please input your last name";
                                            }
                                            else
                                            {
                                                SignUp();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        SignUpWarning.text = "Input a correct date format mm/dd/yyy";
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
        age = (System.DateTime.Now.Year - birthCheck.Year);
        var email = userEmail.text;
        var password = userPassword.text;
        PlayerPrefs.SetString("First Name", firstName.text);
        PlayerPrefs.SetString("Last Name", lastName.text);
        PlayerPrefs.SetString("Email", userEmail.text);
        PlayerPrefs.SetInt("Age",(System.DateTime.Now.Year - birthCheck.Year));
        PlayerPrefs.SetString("Gender", genderVal);

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    SignUpWarning.text = "Email is already in use";
                    SignUpWarning.gameObject.SetActive(true);
                    Debug.LogError(task.Exception);
                    return;
                }
                signUpScreen.SetActive(false);
                loginWarning.gameObject.SetActive(true);
                loginWarning.text = ("Sign Up Completed");
                // Firebase user has been created.
                Firebase.Auth.FirebaseUser newUser = task.Result;
                Firebase.Analytics.FirebaseAnalytics.SetUserProperty("Gender", genderVal);
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                newUser.SendEmailVerificationAsync();
                currUser = newUser;
                PlayerPrefs.SetFloat("Age", Mathf.Round(((System.DateTime.Today - birthCheck).Days) / 365));
                PlayerPrefs.SetString("Gender", genderVal);

            });
    }
    public void loginFailure()
    {
        Debug.LogWarning("TestRun");
        this.GetComponent<AWSManager>().DowloadDataSet("mainmenu");
    }

    //Firebase Login Function
    public void resetPassword()
    {
        if (emailAnalytics != null)
        {
             Firebase.Auth.FirebaseAuth.DefaultInstance.SendPasswordResetEmailAsync(emailAnalytics).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }

    }
    public void LogIn()
    {
        string email;
        string password;
        if (PlayerPrefs.GetString("Status") == "Signed In")
        {
            email = PlayerPrefs.GetString("Email");
            password = PlayerPrefs.GetString("Password");
        }
        else
        {
             email = loginEmail.text;
             password = loginPassword.text;
        }
        if(PlayerPrefs.GetString("Status") != "Signed In")
        {
            PlayerPrefs.SetString("Email", loginEmail.text);
            PlayerPrefs.SetString("Password", loginPassword.text);
        }



        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                Debug.LogError("Failure Run");
                loginWarning.text = "Invalid username or password";
                //loginFailure();
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            currUser = newUser;
            if (newUser.IsEmailVerified)
            {
                PlayerPrefs.SetString("Status", "Signed In");
                Debug.Log("Email Verified");
                this.GetComponent<AWSManager>().DowloadDataSet("mainmenu");
                loginScreen.SetActive(true);
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
