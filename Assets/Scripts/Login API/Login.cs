using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System;

public class Login : MonoBehaviour 
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private GameObject loginButton;
    [SerializeField] private GameObject logoutButton;
    [SerializeField] private TMP_Text usernameText;
    [Header("Status")]
    [SerializeField] private GameObject statusModal;
    [SerializeField] private Image statusBG;
    [SerializeField] private TMP_Text statusText;
    const string continueText = "\n(Press To Continue...)";
    private bool isLogged = false;
    private bool isQuitting = false;
    string loginTime;

    private void Start() 
    {
        checkedLogin();
        //PlayerPrefs.GetString("LoginTime", "");
    }


    private void checkedLogin()
    {
        if(APIManager.Instance.account != null)
        {
            isLogged = true;
            // Show username in the game to indicate that the player has logged in
            usernameText.text = "User " + APIManager.Instance.account.username;
            SwitchButton();
            //StartCoroutine(PostLoginRequest(AutoLoginRequest()));
        }
    }

    void OnApplicationQuit()
    {
        if(isLogged)
        {
            // Post game log when the game is closed without logging out
            StartCoroutine(PostGameLog());
        }
    }

    /// <summary>
    /// Called when the login button is clicked and proccess the login form
    /// </summary>
    public void OnLoginButtonClicked()
    {
        statusText.text = "Logging in...";
        statusBG.raycastTarget = false;
        StartCoroutine(PostLoginRequest(LoginRequest()));
    }

    private UnityWebRequest LoginRequest()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        formData.Add(new MultipartFormDataSection("username", usernameInput.text));

        // Convert password to MD5 hash
        byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(passwordInput.text);
        MD5 md5 = MD5.Create();
        byte[] hashBytes = md5.ComputeHash(passwordBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

        formData.Add(new MultipartFormDataSection("password", hash));

        UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "loginPlayer.php", formData);

        return www;
    }

    // private UnityWebRequest AutoLoginRequest()
    // {
    //     List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
    //     formData.Add(new MultipartFormDataSection("accessToken", APIManager.Instance.previousAccessToken));

    //     UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "login", formData);

    //     return www;
    // }

    /// <summary>
    /// Post the login request to the server and wait for the response
    /// </summary>
    /// <param name="www">Form request to POST</param>
    private IEnumerator PostLoginRequest(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            // Error handling
            string json = www.downloadHandler.text; //get the response as JSON string

            // Check connection error
            if(string.IsNullOrEmpty(json))
            {
                statusText.text = "<size=56>" + www.error;
                statusText.text += continueText;
                statusBG.raycastTarget = true;
            }
            else
            {
                // Convert JSON string to LoginCallback object
                LoginCallback loginCallback = JsonUtility.FromJson<LoginCallback>(json);

                // Show the error message from the server
                statusText.text = "<size=56>" + loginCallback.message;
                statusText.text += continueText;
                statusBG.raycastTarget = true;
            }
            // //dummy
            // Account account = new Account("dummy", "dummy", "dummy");
            // SetAccount(account);
        }
        else
        {
            statusText.text = "Logging in...";
            string json = www.downloadHandler.text; //get the response as JSON string
            Debug.Log(json);

            //Convert JSON string to LoginCallback object
            LoginCallback loginCallback = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginCallback>(json);

            //Convert JWT token to LoginPayload object
            string header = loginCallback.token.Split('.')[0];
            string payload = loginCallback.token.Split('.')[1];
            string signature = loginCallback.token.Split('.')[2];
            string stringPayload = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(Base64StringValidator(payload)));
            LoginPayload loginPayload = JsonUtility.FromJson<LoginPayload>(stringPayload);


            // //Convert JSON string to Account object
            Account account = new Account(loginPayload.data.id_player, loginPayload.data.username, loginPayload.data.nama_player);
            account.SetEventLog(loginCallback.playerprogression);
            APIManager.Instance.SetAccount(account);

            statusText.text = "Login success!";
            statusText.text += continueText;
            statusBG.raycastTarget = true;

            // Try to read save data specific to the account (locally)
            // SaveManager.Instance.ReadSaveDataAccount(account.id);
            // SaveManager.Instance.currentAccountID = account.id;
            // MainMenuManager.Instance.CheckProgress(); //Refresh Main menu continue progress button
            isLogged = true;
            SwitchButton();

            // Show username in the game to indicate that the player has logged in
            usernameText.text = "User " + account.username;

            // Track login time for 'waktu_mulai' in game_log
            loginTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            yield return new WaitForSeconds(1f);
        }
    }

    #region Logout
    /// <summary>
    /// Called when the logout button is clicked and proccess the logout.
    /// (p.s: Add a new event to Activate the status message modal GameObject in the Button's OnClick() event)
    /// </summary>
    public void OnLogoutButtonClicked()
    {
        StartCoroutine(PostGameLog());
        isLogged = false;
        SwitchButton();

        // At this point the status message modal should be active
        statusText.text = "Logout success!";
        statusText.text += continueText;
        statusBG.raycastTarget = true;
        usernameText.text = "";
        APIManager.Instance.SetAccount(null);
    }

    /// <summary>
    /// Post the game log to the server when the player logs out
    /// </summary>
    /// <returns></returns>
    private IEnumerator PostGameLog()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("id_game", APIManager.ID_GAME.ToString()));
        formData.Add(new MultipartFormDataSection("id_player", APIManager.Instance.account.id_player));
        formData.Add(new MultipartFormDataSection("waktu_mulai", loginTime));
        formData.Add(new MultipartFormDataSection("waktu_entry", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

        UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "create_loggame.php", formData);

        yield return www.SendWebRequest();

        // If the request is successful, read the response as JSON string
        // Here we assume that the server will return a log_id as a response that will be used to post the game event log
        // SUBJECT TO CHANGE, Sementara kurang lebih seperti ini
        if(www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text; //get the response as JSON string

            //Convert JSON string to GameLog object
            GameLogCallback gameLog = JsonUtility.FromJson<GameLogCallback>(json);
            Debug.Log("Game log posted! ID: " + gameLog.id_log);

            // Post the game event log to the server

            // StartCoroutine(GameEventLogRequest(QuestManager.Instance.GetQuestEventsLog(), gameLog.id_log));
        }
        else
        {
            Debug.Log(www.error);

            // If the request is not successful, just quit the game if the player is trying to quit
            TryToQuit();
        }
    }

    /// <summary>
    /// Post the game event log to the server when the player logs out
    /// </summary>
    /// <param name="eventLogs"></param>
    /// <returns></returns>
    /// SUBJECT TO CHANGE, Sementara kurang lebih seperti ini
    /// Iki gk efisien dadi haruse diubah, tapi haruse gk digae tiap request dipost dewe-dewe koyok ngene. Iki meloki ferry
    private IEnumerator GameEventLogRequest(List<EventLog> eventLogs, int id_log)
    {
        List<UnityWebRequest> wwws = new List<UnityWebRequest>();
        foreach(EventLog log in eventLogs)
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            // formData.Add(new MultipartFormDataSection("id_game", APIManager.ID_GAME.ToString()));
            formData.Add(new MultipartFormDataSection("id_log", id_log.ToString()));
            formData.Add(new MultipartFormDataSection("no_event", log.no_event.ToString()));
            formData.Add(new MultipartFormDataSection("status_event", log.status.ToString()));

            UnityWebRequest www = UnityWebRequest.Post(APIManager.baseURL + "create_gameevent.php", formData);
            wwws.Add(www);
        }

        foreach(UnityWebRequest www in wwws)
        {
            yield return www.SendWebRequest();
        }

        // If the request is successful, just quit the game if the player is trying to quit
        TryToQuit();
    }
    #endregion


    /// <summary>
    /// Switch the login and logout button active state
    /// </summary>
    private void SwitchButton()
    {
        loginButton.SetActive(!isLogged);
        logoutButton.SetActive(isLogged);
    }
    /// <summary>
    /// Handle quit game button, logout the player if they are logged in and post the game log
    /// </summary>
    /// <param name="base64String"></param>
    /// <returns></returns>
    public void OnQuitGameButtonClicked()
    {
        isQuitting = true;

        if(isLogged)
        {
            statusModal.SetActive(true);
            statusText.text = "Logging out...";
            statusBG.raycastTarget = false;

            StartCoroutine(PostGameLog());
        }
        else
        {
            TryToQuit();
        }
    }

    private void TryToQuit()
    {
        if(isQuitting)
        {
            QuitPrevention.RunOnQuit();
            Application.Quit();
        }
    }


    #region Utility
    private string Base64StringValidator(string base64String)
    {
        while(base64String.Length % 4 != 0)
        {
            base64String += "=";
        }
        return base64String;
    }
    #endregion
}


#region Logout
[Serializable]
public class GameLogCallback
{
    public int status;
    public string message;
    public int id_log;

    public GameLogCallback(int status, string message, int id_log)
    {
        this.status = status;
        this.message = message;
        this.id_log = id_log;
    }

    public GameLogCallback(int status, string message)
    {
        this.status = status;
        this.message = message;
    }
}
#endregion



#region Login
[Serializable]
public class LoginCallback
{
    [JsonProperty("status")] public string status;
    [JsonProperty("message")] public string message;
    [JsonProperty("token")] public string token;
    [JsonProperty("playerprogression")] public List<List<EventLog>> playerprogression;

    public LoginCallback(string status, string message, string jwtToken, List<List<EventLog>> playerprogression)
    {
        this.status = status;
        this.message = message;
        this.token = jwtToken;
        this.playerprogression = playerprogression;
    }

    // public LoginCallback(int status, string message)
    // {
    //     this.status = status;
    //     this.message = message;
    // }
}

[Serializable]
public class LoginPayload
{
    public string iss;
    public int iat;
    public int exp;
    public LoginData data;

    public LoginPayload(string iss, int iat, int exp, LoginData data)
    {
        this.iss = iss;
        this.iat = iat;
        this.exp = exp;
        this.data = data;
    }


    [Serializable]
    public class LoginData
    {
        public string id_player;
        public string nama_player;
        public string username;

        public LoginData(string id_player, string nama_player, string username)
        {
            this.id_player = id_player;
            this.nama_player = nama_player;
            this.username = username;
        }

    }
}
#endregion