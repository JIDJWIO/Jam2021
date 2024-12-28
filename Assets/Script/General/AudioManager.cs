using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMAudioSource;
    public GameObject FXAudioSources;
    [Header("ÊÂ¼þ¼àÌý")]
    public AudioEventSO BGMEvent;
    public AudioEventSO FXEvent;
    
    private void Awake()
    {
        BGMEvent.OnEventRaised += OnBGMEvent;
        FXEvent.OnEventRaised += OnFXEvent;
    }  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        BGMEvent.OnEventRaised -= OnBGMEvent;
        FXEvent.OnEventRaised -= OnFXEvent;
    }

    private void OnBGMEvent(AudioClip audioClip)
    {
        BGMAudioSource.clip = audioClip;
        BGMAudioSource.Play();
    }
    private void OnFXEvent(AudioClip audioClip)
    {
        GameObject obj = GameObject.Instantiate(FXAudioSources,Vector3.zero,Quaternion.identity);
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.Play();
        StartCoroutine(DestroyAudioSource(obj, audioClip.length));
    }   
    IEnumerator DestroyAudioSource(GameObject sourceObj, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(sourceObj);
    }
}
