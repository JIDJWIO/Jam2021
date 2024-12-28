using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Jam/LevelTable", fileName = "LevelTable")]
public class LevelTable : ScriptableObject
{
    public List<LevelTableItem> items=new List<LevelTableItem>();
    public LevelTableItem GetLevelTableItemWithId(int id)
    {
        foreach (var item in items)
        {
            if (item.id == id)
                return item;
        }
        return null;
    }
}

[Serializable]
public class LevelTableItem
{
    public int id;
    public Vector2 playerPosition;
}
