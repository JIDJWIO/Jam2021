using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectLevelMenu : MonoBehaviour
{
    public ChangeSceneEventSO ChangeSceneEvent;
    public UICanvasEventSO UICanvasEvent;
    public ToolTipEventSO ToolTipEvent;
    public Text level1StateText;
    public Text level2StateText;
    public Text level3StateText;
    public Text level4StateText;
    public Text level5StateText;

    private void OnEnable()
    {
        bool n=LevelLocalData.instance.levelData.level1;
        //展示各个关卡的状态
        level1StateText.text = LevelLocalData.instance.levelData.level1 ? "已解锁" : "未解锁";
        level2StateText.text = LevelLocalData.instance.levelData.level2 ? "已解锁" : "未解锁";
        level3StateText.text = LevelLocalData.instance.levelData.level3 ? "已解锁" : "未解锁";
        level4StateText.text = LevelLocalData.instance.levelData.level4 ? "已解锁" : "未解锁";
        level5StateText.text = LevelLocalData.instance.levelData.level5 ? "已解锁" : "未解锁";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToLevel1()
    {
        if(LevelLocalData.instance.levelData.level1)
        {
            ChangeSceneEvent.RaiseEvent(Level.Level1);
            UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.Ignore);
        }
        else
        {
            //提示未解锁
            ToolTipEvent.RaiseEvent("关卡未解锁", 1);
        }
    }
    public void GoToLevel2()
    {
        if (LevelLocalData.instance.levelData.level2)
        {
            ChangeSceneEvent.RaiseEvent(Level.Level2);
            UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.Ignore);
        }
        else
        {
            //提示未解锁
            ToolTipEvent.RaiseEvent("关卡未解锁", 1);
        }
    }
    public void GoToLevel3()
    {
        if (LevelLocalData.instance.levelData.level3)
        {
            ChangeSceneEvent.RaiseEvent(Level.Level3);
            UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.Ignore);
        }
        else
        {
            //提示未解锁
            ToolTipEvent.RaiseEvent("关卡未解锁", 1);
        }
    }
    public void GoToLevel4()
    {
        if (LevelLocalData.instance.levelData.level4)
        {
            ChangeSceneEvent.RaiseEvent(Level.Level4);
            UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.Ignore);
        }
        else
        {
            //提示未解锁
            ToolTipEvent.RaiseEvent("关卡未解锁", 1);
        }
    }
    public void GoToLevel5()
    {
        if (LevelLocalData.instance.levelData.level5)
        {
            ChangeSceneEvent.RaiseEvent(Level.Level5);
            UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.Ignore);
        }
        else
        {
            //提示未解锁
            ToolTipEvent.RaiseEvent("关卡未解锁", 1);
        }
    }
    public void ReturnToMainMenu()
    {
        UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.MainMenu);
    }
}
