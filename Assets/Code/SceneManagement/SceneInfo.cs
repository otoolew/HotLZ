// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: Starship
//  SEPT 2018
// ----------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "newSceneAsset", menuName = "Scene Managment/SceneAsset")]
public class SceneInfo : ScriptableObject
{
    [SerializeField] private string sceneName;
    public string SceneName { get => sceneName; private set => sceneName = value; }
    [TextArea]
    public string sceneDescription;
#if UNITY_EDITOR
    public UnityEditor.SceneAsset Scene;
    private void OnValidate()
    {
        //collect the scene name
        if (Scene != null)
            SceneName = Scene.name;
        else
            SceneName = "";
    }
#endif
}
