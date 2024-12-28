using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMenu : MonoBehaviour
{
    public UICanvasEventSO UICanvasEvent;
    public SetPlayerControllerInputEventSO SetPlayerControllerInputEvent;
    public void CloseStopMenu()
    {
        UICanvasEvent.RaiseEvent(UICanvas.StopMenu, UICanvas.Ignore);
        SetPlayerControllerInputEvent.RaiseEvent(true);
    }
    public void BackToSLM()
    {
        UICanvasEvent.RaiseEvent(UICanvas.StopMenu, UICanvas.SelectLevelMenu);
    }
}
