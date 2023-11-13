using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;

    [SerializeField] private Button continueGameButton;

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.interactable = false;
        }
    }

    public void PlayGame()
    {
        DisableMenuButtons();
        // create a new game - which will initialize our game data
        DataPersistenceManager.instance.NewGame();
        // Load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Intro");
        //SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void ContinueGame()
    {
        DisableMenuButtons();
        // Load the gameplay scene - which will in turn save the game because of
        // OnSceneUnloaded() in the DataPersistenceManager
        SceneManager.LoadSceneAsync("Intro");
    }

    public void TutorialGame()
    {
        SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void DisableMenuButtons()
    {
        newGameButton.interactable = false;
        continueGameButton.interactable = false;
    }
}