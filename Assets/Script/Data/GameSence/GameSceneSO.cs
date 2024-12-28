using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="Jam/GameSceneSO",fileName ="GameSceneSO")]
[System.Serializable]
public class GameSceneSO : ScriptableObject
{
    public AssetReference senseReference;
}
