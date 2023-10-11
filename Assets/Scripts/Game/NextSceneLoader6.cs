using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader6 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Pidato", LoadSceneMode.Single);
    }
}
