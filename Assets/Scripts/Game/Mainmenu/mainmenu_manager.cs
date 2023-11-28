using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{

    public Button ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        GameData data = SaveManager.instance.LoadSceneData();
        if (data != null)
        {
            ContinueButton.interactable = true;
        }
        else
        {
            ContinueButton.interactable = false;
        }        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
        if(APIManager.Instance.account != null)
        {
            APIManager.Instance.account.setEventNewAccount();
        }
    }

    public void ContinueGame()
    {
        LoadSceneData();
    }

    private void LoadSceneData()
    {
        GameData data = SaveManager.instance.LoadSceneData();
        if (data != null)
        {
            SceneManager.LoadScene(data.sceneName);
        }
        else
        {
            Debug.LogWarning("No save data found.");
        }
    }

    public void TutorialGame()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void CreditGame()
    {
        SceneManager.LoadScene("Credit", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}