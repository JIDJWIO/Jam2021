using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FadeManager : MonoBehaviour
{
    public UnityEngine.UI.Image fadeImage;
    public float disappearTime;
    public float appearTime;
    [Header("事件监听")]
    public FadeEventSO FadeEvent;

    int fadeState;
    Color fadeColor;
    private void OnEnable()
    {
        FadeEvent.OnEventRaised += ChangeFadeState;
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //fade透明度变化逻辑
        fadeColor = fadeImage.color;
        switch (fadeState)
        {
            case -1:
                {
                    float v = 1 / disappearTime;
                    if (fadeColor.a + fadeState * v * Time.deltaTime <= 0)
                    {
                        fadeColor.a = 0;
                        fadeImage.color = fadeColor;
                    }
                    else
                    {
                        fadeColor.a = fadeColor.a + fadeState * v * Time.deltaTime;
                        fadeImage.color = fadeColor;
                    }
                    break;
                }
            case 1:
                {
                    float v = 1 / appearTime;
                    if (fadeColor.a + fadeState * v * Time.deltaTime >= 1)
                    {
                        fadeColor.a = 1;
                        fadeImage.color = fadeColor;
                    }
                    else
                    {
                        fadeColor.a = fadeColor.a + fadeState * v * Time.deltaTime;
                        fadeImage.color = fadeColor;
                    }
                    break;
                }
        }
    }



    private void OnDisable()
    {
        FadeEvent.OnEventRaised -= ChangeFadeState;
    }
    private void ChangeFadeState(int fs)
    {
        fadeState = fs;
    }
}
