using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader8 : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("Intropertempuran", LoadSceneMode.Single);
    }
}
