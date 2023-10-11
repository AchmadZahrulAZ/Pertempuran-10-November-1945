using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader10 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Mainmenu", LoadSceneMode.Single);
    }
}
