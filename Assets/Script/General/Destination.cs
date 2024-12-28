using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioEventSO FXEvent;
    public Level levelToUnlock;
    public float waitAniTime;
    public OpenFinishedMenuEventSO OpenFinishedMenuEvent;
    public UICanvasEventSO UICanvasEvent;
    public SetPlayerControllerInputEventSO SetPlayerControllerInputEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            //����Ҳ��ܱ�����
            SetPlayerControllerInputEvent.RaiseEvent(false);
            
            //������һ��
            switch (levelToUnlock)
            {
                case Level.Level1:
                    {
                        LevelLocalData.instance.levelData.level1 = true;
                        break;
                    }
                case Level.Level2:
                    {
                        LevelLocalData.instance.levelData.level2 = true;
                        break;
                    }
                case Level.Level3:
                    {
                        LevelLocalData.instance.levelData.level3 = true;
                        break;
                    }
                case Level.Level4:
                    {
                        LevelLocalData.instance.levelData.level4 = true;
                        break;
                    }
                case Level.Level5:
                    {
                        LevelLocalData.instance.levelData.level5 = true;
                        break;
                    }
            }
            //�ȴ�һ�´�ͨ��UI
            StartCoroutine("OpenFinishedMenu");
        }
    }
    IEnumerator OpenFinishedMenu()
    {
        yield return new WaitForSeconds(waitAniTime);
        FXEvent.RaiseEvent(audioClip);
        //��FinishedMenu��ΪFinishedMenu�ڵ�levelToGo��ֵ
        UICanvasEvent.RaiseEvent(UICanvas.Ignore, UICanvas.FinishedMenu);
        OpenFinishedMenuEvent.RaiseEvent(levelToUnlock);
    }
}
