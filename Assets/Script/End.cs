using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioEventSO FXEvent;
    public ToolTipEventSO ToolTipEvent;
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
            ToolTipEvent.RaiseEvent("�������һ�أ���ϲ��ɹ�ͨ�أ�", 2);
            FXEvent.RaiseEvent(audioClip);
        }
    }
}
