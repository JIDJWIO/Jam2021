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

    [Header("死亡时执行")]
    public UnityEvent OnDeath;
    [Header("受伤时执行")]
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
    //在被攻击时执行
    public void Hitted(int damage)
    {
        currentLife -= damage;
        if(currentLife<=0&&!isDeath)
        {
            //执行死亡逻辑
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
            //执行受伤逻辑
            OnHitted.Invoke();
            
        }
    }
    public void Resurrected()
    {
        isDeath = false;
    }
}
