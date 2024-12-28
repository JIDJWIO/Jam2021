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
        //չʾ�����ؿ���״̬
        level1StateText.text = LevelLocalData.instance.levelData.level1 ? "�ѽ���" : "δ����";
        level2StateText.text = LevelLocalData.instance.levelData.level2 ? "�ѽ���" : "δ����";
        level3StateText.text = LevelLocalData.instance.levelData.level3 ? "�ѽ���" : "δ����";
        level4StateText.text = LevelLocalData.instance.levelData.level4 ? "�ѽ���" : "δ����";
        level5StateText.text = LevelLocalData.instance.levelData.level5 ? "�ѽ���" : "δ����";
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
            //��ʾδ����
            ToolTipEvent.RaiseEvent("�ؿ�δ����", 1);
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
            //��ʾδ����
            ToolTipEvent.RaiseEvent("�ؿ�δ����", 1);
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
            //��ʾδ����
            ToolTipEvent.RaiseEvent("�ؿ�δ����", 1);
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
            //��ʾδ����
            ToolTipEvent.RaiseEvent("�ؿ�δ����", 1);
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
            //��ʾδ����
            ToolTipEvent.RaiseEvent("�ؿ�δ����", 1);
        }
    }
    public void ReturnToMainMenu()
    {
        UICanvasEvent.RaiseEvent(UICanvas.SelectLevelMenu, UICanvas.MainMenu);
    }
}
