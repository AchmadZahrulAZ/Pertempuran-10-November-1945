using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader9 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Pertempuran", LoadSceneMode.Single);
    }
}
