using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        //����Local����
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
        //����Local����
        LevelLocalData.instance.Save();
    }
}
