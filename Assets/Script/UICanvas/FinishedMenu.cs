using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedMenu : MonoBehaviour
{
    public Level levelToGo;
    public ChangeSceneEventSO ChangeSceneEvent;
    public UICanvasEventSO UICanvasEvent;
    public SetPlayerControllerInputEventSO SetPlayerControllerInputEvent;
    [Header("事件监听")]
    public OpenFinishedMenuEventSO OpenFinishedMenuEvent;

    private void Awake()
    {
        OpenFinishedMenuEvent.OnEventRaised += SetLevelToGo;
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
        OpenFinishedMenuEvent.OnEventRaised -= SetLevelToGo;
    }
    public void OpenNextLevel()
    {
        //让玩家可以被控制
        SetPlayerControllerInputEvent.RaiseEvent(true);

        ChangeSceneEvent.RaiseEvent(levelToGo);
        UICanvasEvent.RaiseEvent(UICanvas.FinishedMenu, UICanvas.Ignore);

    }
    void SetLevelToGo(Level level)
    {
        levelToGo = level;
    }
}
