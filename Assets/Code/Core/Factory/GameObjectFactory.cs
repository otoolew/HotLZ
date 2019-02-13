using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectFactory
{
    public static GameObject InstantiatePrefab(GameObjectSchematic gameObjectSchematic)
    {
        GameObject gameObject = GameObject.Instantiate(gameObjectSchematic.gameObjectPrefab);
        return gameObject;
    }
}
