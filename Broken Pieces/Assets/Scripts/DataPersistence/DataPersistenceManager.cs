using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Conig")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public static DataPersistenceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one Data Persistence Manager in scene, please rectify.");
        }
        Instance = this;
    }
    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
        SceneManager.LoadScene("Level 1");
    }
    public void LoadGame()
    {
        //load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();
        //If no data found will initialze to a new game
        if (this.gameData == null)
        {
            Debug.Log("No game data was found. Initializing data to defaults.");
            NewGame();
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded Level = " + gameData.currentLevel);
    }
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        Debug.Log("Saved Level = " + gameData.currentLevel);

        //save that data to a file using the data handler
        dataHandler.Save(gameData);
    }
    public void SaveAndQuitToMenu()
    {
        SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Saved and quit to main menu");
    }
    private void OnApplicationQuit()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SaveGame();
        }
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistencesObjects);
    }
}