using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/OpenFinishedMenuEventSO")]
public class OpenFinishedMenuEventSO : ScriptableObject
{
    public UnityAction<Level> OnEventRaised;
    public void RaiseEvent(Level level)
    {
        OnEventRaised.Invoke(level);
    }
}
