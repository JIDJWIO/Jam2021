using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;

public class GMCmd
{
    [MenuItem("GMCmd/Build Addressables")]
    public static void BuildAddressables()
    {
        AddressableAssetSettings.BuildPlayerContent();
        EditorUtility.DisplayDialog("Addressables Build", "Addressables build completed!", "OK");
    }
    [MenuItem("GMCmd/创建关卡数据")]
    public static void CreateLevelLocalData()
    {
        LevelLocalData.instance.levelData = new LevelData();

        LevelLocalData.instance.levelData.level1 = true;
        LevelLocalData.instance.levelData.level2 = false;
        LevelLocalData.instance.levelData.level3 = false;
        LevelLocalData.instance.levelData.level4 = false;
        LevelLocalData.instance.levelData.level5 = false;

        LevelLocalData.instance.Save();
    }
    [MenuItem("GMCmd/读取关卡数据")]
    public static void ReadLevelLocalData()
    {
        LevelLocalData.instance.Load();
        LevelData levelData = LevelLocalData.instance.levelData;

        Debug.Log("Level1:" + levelData.level1);
        Debug.Log("Level2:" + levelData.level2);
        Debug.Log("Level3:" + levelData.level3);
        Debug.Log("Level4:" + levelData.level4);
        Debug.Log("Level5:" + levelData.level5);
    }
}
