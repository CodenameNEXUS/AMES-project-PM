using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int currentLevel;

    //Values defined here will be the default values
    public GameData()
    {
        this.currentLevel = 0;
    }
}
