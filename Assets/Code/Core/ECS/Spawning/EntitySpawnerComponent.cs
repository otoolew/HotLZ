using System;
using Unity.Entities;
using UnityEngine;

[Serializable]
public struct EntitySpawner : ISharedComponentData
{
    public GameObject prefab;
    public int count;
}

public class EntitySpawnerComponent : SharedComponentDataWrapper<EntitySpawner> { } // Editor Support