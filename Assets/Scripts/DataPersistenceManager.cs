using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{

    [Header("File Storage Config")]

    [SerializeField] private string fileName;

    [SerializeField] private bool useEncryption;

    private GameData gameData;

    private List<IDataPersistance> dataPersistanceObjects;

    private FileDataHandler dataHandler;

    public static DataPersistanceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistance Manager in the Scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(OnApplicationQuit.persistenceDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistanceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    } 

    public void LoadGame()
    {
        // TODO - Load any saved data from a file using the data handler

        this.gameData = dataHandler.Load();

        // if no data can be loaded, don't continue
        if (this.gameData == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
            return;
        }

        // TODO - push the Loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistanceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // if we don't have any data to save, log a warning here
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
            return;
        }

        // TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistanceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        // TODO - save that data to a file using the data handler
        dataHandler.Save(gameData); 
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistanceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

}