// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: Starship
//  SEPT 2018
// ----------------------------------------------------------------------------
using UnityEngine;
/// <summary>
/// ScriptableObject that contains SceneInfo
/// </summary>
[CreateAssetMenu(fileName = "newSceneAsset", menuName = "Scene Managment/SceneAsset")]
public class SceneAsset : ScriptableObject {
    public SceneInfo sceneInfo;
}
