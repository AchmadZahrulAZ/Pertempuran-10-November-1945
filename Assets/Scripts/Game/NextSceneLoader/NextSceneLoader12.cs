using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader12 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Mainmenu", LoadSceneMode.Single);
    }
}
