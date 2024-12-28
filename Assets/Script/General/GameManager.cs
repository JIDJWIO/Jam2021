using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        //加载Local数据
        LevelLocalData.instance.Load();
        LevelLocalData.instance.levelData.level1 = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        //保存Local数据
        LevelLocalData.instance.Save();
    }
}
