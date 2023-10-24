using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Update()
    {
        // Cari semua objek dengan tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Jika tidak ada musuh yang tersisa
        if (enemies.Length == 0)
        {
            // Pemain telah habis atau musuh telah habis
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Penutup", LoadSceneMode.Single);
    }
}
