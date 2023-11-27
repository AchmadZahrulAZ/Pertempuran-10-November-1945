using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader9a : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Pertempuran_nogun", LoadSceneMode.Single);       
    }
}
