using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader3 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Penutup", LoadSceneMode.Single);
    }
}
