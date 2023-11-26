using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader5 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Pemukimanwarga", LoadSceneMode.Single);
    }
}
