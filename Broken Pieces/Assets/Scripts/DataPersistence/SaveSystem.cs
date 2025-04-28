using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    string keyWord = "123456789";
    public void NewGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void SaveGame()
    {
        SaveData myData = new SaveData();
        myData.savedLevel = SceneManager.GetActiveScene().name;
        string myDataString = JsonUtility.ToJson(myData);
        myDataString = EncryptDecryptData(myDataString);
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        System.IO.File.WriteAllText(file, myDataString);
        Debug.Log(file);
    }

    public void LoadGame()
    {
        string file = Application.persistentDataPath + "/" + gameObject.name + ".json";
        if (File.Exists(file))
        {
            string jsonData = File.ReadAllText(file);
            jsonData = EncryptDecryptData(jsonData);
            Debug.Log(jsonData);
            SaveData myData = JsonUtility.FromJson<SaveData>(jsonData);
            SceneManager.LoadScene(myData.savedLevel);
            Debug.Log("Loaded Scene = " + myData.savedLevel);
        }
    }

    public string EncryptDecryptData(string data)
    {
        string result = "";
        for (int i = 0; i < data.Length; i++)
        {
            result += (char)(data[i] ^ keyWord[i % keyWord.Length]);
        }
        Debug.Log(result);
        return result;
    }
    public void SaveAndQuitToMenu()
    {
        SaveGame();
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Saved and quit to Main Menu");
    }
    private void OnApplicationQuit()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SaveGame();
        }
    }
}

[System.Serializable]
public class SaveData
{
    public string savedLevel;
}