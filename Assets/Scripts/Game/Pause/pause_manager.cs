using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField]private string sceneName = "";
    [SerializeField]private int sceneIndex = 0;

    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        GameData data = new GameData(sceneName, sceneIndex, QuizManager.Instance.answers);
        SaveManager.instance.SaveSceneData(data);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Mainmenu", LoadSceneMode.Single);
    }
}