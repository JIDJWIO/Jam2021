using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class Main : MonoBehaviour
{
    public GameSceneSO persistentSO;
    // Start is called before the first frame update
    void Start()
    {
        persistentSO.senseReference.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
