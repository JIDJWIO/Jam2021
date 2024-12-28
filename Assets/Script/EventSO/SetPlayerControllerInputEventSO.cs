using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SetPlayerControllerInputEventSO")]
public class SetPlayerControllerInputEventSO : ScriptableObject
{
    public UnityAction<bool> OnEventRaised;
    public void RaiseEvent(bool state)
    {
        OnEventRaised.Invoke(state);
    }
}
