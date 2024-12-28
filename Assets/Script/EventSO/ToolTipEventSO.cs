using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/ToolTipEventSO")]
public class ToolTipEventSO : ScriptableObject
{
    public UnityAction<string, float> OnEventRaised;
    public void RaiseEvent(string text,float time)
    {
        OnEventRaised.Invoke(text, time);
    }
}
