using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader11 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Statistik", LoadSceneMode.Single);
    }
}
