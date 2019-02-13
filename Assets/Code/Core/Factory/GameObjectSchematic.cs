using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newGameObject", menuName = "Factory/GameObject")]
public class GameObjectSchematic : ScriptableObject
{
    public GameObject gameObjectPrefab;
}
