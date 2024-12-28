using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioEventSO BGMEvent;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        BGMEvent.RaiseEvent(audioClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
