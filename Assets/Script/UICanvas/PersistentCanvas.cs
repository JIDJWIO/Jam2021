using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentCanvas : MonoBehaviour
{
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
    public void OpenStopMenu()
    {
        UICanvasEvent.RaiseEvent(UICanvas.Ignore, UICanvas.StopMenu);
        SetPlayerControllerInputEvent.RaiseEvent(false);
    }
}
