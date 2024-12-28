using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioEventSO FXEvent;
    public int maxLife;
    public int currentLife;
    public bool dontDesOnDie;

    [Header("����ʱִ��")]
    public UnityEvent OnDeath;
    [Header("����ʱִ��")]
    public UnityEvent OnHitted;

    bool isDeath;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxLife;
        isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�ڱ�����ʱִ��
    public void Hitted(int damage)
    {
        currentLife -= damage;
        if(currentLife<=0&&!isDeath)
        {
            //ִ�������߼�
            FXEvent.RaiseEvent(audioClip);
            isDeath = true;
            OnDeath.Invoke();
            if (!dontDesOnDie)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            //ִ�������߼�
            OnHitted.Invoke();
            
        }
    }
    public void Resurrected()
    {
        isDeath = false;
    }
}
