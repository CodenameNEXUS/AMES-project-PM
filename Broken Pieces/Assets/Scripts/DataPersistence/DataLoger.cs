using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoger : MonoBehaviour, IDataPersistence
{
    private string savedLevel = "Level 1";
    public void LoadData(GameData data)
    {
        this.savedLevel = data.currentLevel;
    }
    public void SaveData(ref GameData data)
    {
        data.currentLevel = SceneManager.GetActiveScene().name;
    }
}
