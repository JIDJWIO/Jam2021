using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public Transform playerTransform;
    
    public FadeEventSO FadeEvent;
    public LevelTable levelTable;
    public float sceneLoadTime;
    public SetAircrftPositionEventSO SetAircrftPositionEvent;
    [Header("事件监听")]
    public ChangeSceneEventSO ChangeSceneEvent;

    Vector2 playerPosition;
    int currentLevelId;
    // Start is called before the first frame update
    void Start()
    {
        ChangeSceneEvent.OnEventRaised+= OnChangeSceneEvent;
        currentLevelId = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        ChangeSceneEvent.OnEventRaised -= OnChangeSceneEvent;
    }
    void OnChangeSceneEvent(Level level)
    {
        //场景切换时开关fade
        FadeEvent.RaiseEvent(1);
        StartCoroutine("CloseFade");
        //关闭当前关卡
        if(currentLevelId != 0)
            SceneManager.UnloadSceneAsync(currentLevelId);
        //打开新的关卡
        switch (level)
        {
            case Level.Level1:
                {
                    currentLevelId = 1;
                    LoadScene();
                    break;
                }
            case Level.Level2:
                {
                    currentLevelId = 2;
                    LoadScene();
                    break;
                }
            case Level.Level3:
                {
                    currentLevelId = 3;
                    LoadScene();
                    break;
                }
            case Level.Level4:
                {
                    currentLevelId = 4;
                    LoadScene();
                    break;
                }
            case Level.Level5:
                {
                    currentLevelId = 5;
                    LoadScene();
                    break;
                }
        }
    }

   

    IEnumerator CloseFade()
    {
        yield return new WaitForSeconds(sceneLoadTime);
        FadeEvent.RaiseEvent(-1);
    }
    Vector2 GetPlyerPosition(int n)
    {
        return levelTable.GetLevelTableItemWithId(n).playerPosition;
    }
    public void ReLoadCurrentScene()
    {
        //关闭当前关卡
        if(currentLevelId != 0)
        {
            var v = SceneManager.UnloadSceneAsync(currentLevelId);
            v.completed += ReLoadScene;
        }       
        
    }
    public void CloseCurrentScene()
    {
        //关闭当前关卡
        SceneManager.UnloadSceneAsync(currentLevelId);
        currentLevelId = 0;
    }
    private void ReLoadScene(AsyncOperation obj)
    {
        FadeEvent.RaiseEvent(1);
        StartCoroutine("CloseFade");
        LoadScene();
    }

    void LoadScene()
    {
        var a = SceneManager.LoadSceneAsync(currentLevelId, LoadSceneMode.Additive);
        
        playerPosition = GetPlyerPosition(currentLevelId);
        a.completed += OnLoadFinished;
    }

    private void OnLoadFinished(AsyncOperation obj)
    {
        StartCoroutine("LateSetPlayerPosition");
    }

    IEnumerator LateSetPlayerPosition()
    {
        yield return new WaitForSeconds(1);
        playerTransform.position = playerPosition;
        playerTransform.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SetAircrftPositionEvent.RaiseEvent();
    }
}
