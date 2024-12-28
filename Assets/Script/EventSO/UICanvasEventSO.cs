using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/UICanvasEventSO")]
public class UICanvasEventSO : ScriptableObject
{
    public UnityAction<UICanvas,UICanvas> OnEventRaised;
    public void RaiseEvent(UICanvas close,UICanvas open)
    {
        OnEventRaised.Invoke(close, open);
    }
}
