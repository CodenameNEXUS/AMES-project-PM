using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoger : MonoBehaviour, IDataPersistence
{
    public void LoadData(GameData data)
    {
        SceneManager.LoadScene(data.currentLevel);
        Debug.Log("Loaded Level " + data.currentLevel);
    }
    public void SaveData(ref GameData data)
    {
        data.currentLevel = SceneManager.GetActiveScene().name;
    }
}
