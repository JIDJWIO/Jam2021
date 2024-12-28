using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public UICanvasEventSO UICanvasEvent;
    public GameObject courseImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        UICanvasEvent.RaiseEvent(UICanvas.MainMenu, UICanvas.SelectLevelMenu);
    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void OpenCourseImage()
    {
        courseImage.SetActive(true);
    }
    public void CloseCourseImage()
    {
        courseImage.SetActive(false);
    }
}
