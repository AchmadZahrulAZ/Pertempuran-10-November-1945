using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader7 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Ruangsiaran", LoadSceneMode.Single);
    }
}
