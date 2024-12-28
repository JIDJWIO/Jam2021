using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameSceneSO persistentGameSceneSO;
    // Start is called before the first frame update
    void Start()
    {
        persistentGameSceneSO.senseReference.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
