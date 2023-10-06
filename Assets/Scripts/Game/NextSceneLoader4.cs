using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader4 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }
}
