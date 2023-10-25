using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void TutorialGame()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}