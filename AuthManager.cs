using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class AuthManager : MonoBehaviour
{
    [Header("Register UI")]
    public TMP_InputField regEmail;
    public TMP_InputField regPassword;
    public TMP_Text registerStatus;
    public Button registerButton;
    public Button returnButtonRegister;

    [Header("Login UI")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginPassword;
    public TMP_Text loginStatus;
    public Button loginButton;
    public Button returnButtonLogin;

    [Header("Canvases")]
    public GameObject registerCanvas;
    public GameObject loginCanvas;

    [Header("Other UI")]
    public Button startButton;

    private string registerUrl = "https://authserver-j7rg.onrender.com/register";
    private string loginUrl = "https://authserver-j7rg.onrender.com/login";
    private bool isLoggedIn = false;

    void Start()
    {
        isLoggedIn = PlayerPrefs.GetInt("isLoggedIn", 0) == 1;
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(false);

        if (startButton != null) 
        {
            startButton.interactable = isLoggedIn;
        }

        if (loginStatus != null)
        {
            loginStatus.text = "";
            loginStatus.gameObject.SetActive(false);
        }
        if (registerStatus != null)
        {
            registerStatus.text = "";
            registerStatus.gameObject.SetActive(false);
        }

        if (returnButtonRegister != null) returnButtonRegister.onClick.AddListener(OnReturnButtonClicked);
        if (returnButtonLogin != null) returnButtonLogin.onClick.AddListener(OnReturnButtonClicked);
    }

    public void OnRegisterButtonClicked()
    {
        StartCoroutine(RegisterUser());
    }

    IEnumerator RegisterUser()
    {
        registerStatus.gameObject.SetActive(false);

        // Create UserCredentials object
        UserCredentials credentials = new UserCredentials { email = regEmail.text, password = regPassword.text };
        string jsonData = JsonUtility.ToJson(credentials);
        Debug.Log("Sending request: " + jsonData);

        UnityWebRequest www = new UnityWebRequest(registerUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            registerStatus.text = "Registration error: " + www.downloadHandler.text;
            registerStatus.gameObject.SetActive(true);
            Debug.LogError("Request failed: " + www.error);
        }
        else
        {
            registerStatus.text = "Account created successfully!";
            registerStatus.gameObject.SetActive(true);
            Debug.Log("Request successful: " + www.downloadHandler.text);
        }
    }

    public void OnLoginButtonClicked()
    {
        StartCoroutine(LoginUser());
    }

    IEnumerator LoginUser()
    {
        loginStatus.gameObject.SetActive(false);

        UserCredentials credentials = new UserCredentials { email = loginEmail.text, password = loginPassword.text };
        string jsonData = JsonUtility.ToJson(credentials);

        UnityWebRequest www = new UnityWebRequest(loginUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            loginStatus.text = "Login error: " + www.downloadHandler.text;
            loginStatus.gameObject.SetActive(true);
        }
        else
        {
            loginStatus.text = "Login successful!";
            loginStatus.gameObject.SetActive(true);
            PlayerPrefs.SetInt("isLoggedIn", 1);
            if (startButton != null) startButton.interactable = true;
        }
    }

    public void OnReturnButtonClicked()
    {
        if (loginCanvas.activeSelf)
        {
            loginCanvas.SetActive(false);
        }
        else if (registerCanvas.activeSelf)
        {
            registerCanvas.SetActive(false);
        }
    }

    public void Logout()
    {
        isLoggedIn = false;
        PlayerPrefs.SetInt("isLoggedIn", 0);
        PlayerPrefs.Save();

        if (startButton != null) startButton.interactable = false;
        loginStatus.text = "Logged out!";
        loginStatus.gameObject.SetActive(true);
    }
}

[System.Serializable]
public class UserCredentials
{
    public string email;
    public string password;
}