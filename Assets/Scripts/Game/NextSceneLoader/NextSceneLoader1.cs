using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader1 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Mallaby", LoadSceneMode.Single);
    }
}