using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLocalData
{
    private static LevelLocalData _instance;
    public static LevelLocalData instance
    {
        get
        {
            if (_instance == null)
                _instance = new LevelLocalData();
            return _instance;
        }
    }

    public LevelData levelData;

    public void Save()
    {
        //序列化存入
        string inventoryJson = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("LevelLocalData", inventoryJson);
        PlayerPrefs.Save();
    }
    public void Load()
    {
        //读出后反序列化
        if(levelData==null)
        {
            string inventoryJson = PlayerPrefs.GetString("LevelLocalData");
            LevelLocalData levelLocalData = JsonUtility.FromJson<LevelLocalData>(inventoryJson);
            levelData = levelLocalData.levelData;
        }
    }
}

[System.Serializable]
public class LevelData
{
    public bool level1, level2,level3,level4,level5;
}
