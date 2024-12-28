using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas StopMenu;
    public Canvas SelectLevelMenu;
    public Canvas FinishedMenu;
    public Canvas MainMenu;
    [Header("ÊÂ¼þ¼àÌý")]
    public UICanvasEventSO UICanvasEvent;

    private void OnEnable()
    {
        UICanvasEvent.OnEventRaised += OnCanvasEvent;
    }
    private void OnDisable()
    {
        UICanvasEvent.OnEventRaised -= OnCanvasEvent;
    }
    void OnCanvasEvent(UICanvas close,UICanvas open)
    {
        switch(close)
        {
            case UICanvas.Ignore:
                {
                    break;
                }
            case UICanvas.StopMenu:
                {
                    StopMenu.gameObject.SetActive(false);
                    break;
                }
            case UICanvas.SelectLevelMenu:
                {
                    SelectLevelMenu.gameObject.SetActive(false);
                    break;
                }
            case UICanvas.FinishedMenu:
                {
                    FinishedMenu.gameObject.SetActive(false);
                    break;
                }
            case UICanvas.MainMenu:
                {
                    MainMenu.gameObject.SetActive(false);
                    break;
                }
        }
        switch (open)
        {
            case UICanvas.Ignore:
                {
                    break;
                }
            case UICanvas.StopMenu:
                {
                    StopMenu.gameObject.SetActive(true);
                    break;
                }
            case UICanvas.SelectLevelMenu:
                {
                    SelectLevelMenu.gameObject.SetActive(true);
                    break;
                }
            case UICanvas.FinishedMenu:
                {
                    FinishedMenu.gameObject.SetActive(true);
                    break;
                }
            case UICanvas.MainMenu:
                {
                    MainMenu.gameObject.SetActive(true);
                    break;
                }
        }
    }
}
