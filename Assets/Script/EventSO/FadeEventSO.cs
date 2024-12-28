using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;
    public void RaiseEvent(int fadeState)
    {
        OnEventRaised?.Invoke(fadeState);
    }
}
