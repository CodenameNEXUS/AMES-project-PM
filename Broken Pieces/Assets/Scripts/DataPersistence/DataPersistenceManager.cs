using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    private GameData gameData;
    public static DataPersistenceManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("More than one Data Persistence Manager in scene, please rectify.");
        }
        Instance = this;
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    public void LoadGame()
    {
        //If no data found will initialze to a new game
        if (this.gameData == null)
        {
            Debug.Log("No game data was found. Initializing data to defaults.");
            NewGame();
        }
    }
    public void SaveGame()
    {

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
}