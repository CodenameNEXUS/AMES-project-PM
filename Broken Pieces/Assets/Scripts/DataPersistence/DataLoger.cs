using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoger : MonoBehaviour, IDataPersistence
{
    private string savedLevel = "Trans Girl Superiority!!";
    public void LoadData(GameData data)
    {
        this.savedLevel = data.currentLevel;
    }
    public void SaveData(ref GameData data)
    {
        //data.currentLevel = this.savedLevel;
        data.currentLevel = "Trans Girl Superiority!!";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            savedLevel = "Trans Girl Superiority!!";
            Debug.Log(savedLevel);
        }
    }
}
