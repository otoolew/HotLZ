using System;
using Unity.Entities;
using UnityEngine;
// Serializable attribute is for editor support.
[Serializable]
public struct MovementSpeed : IComponentData
{
    public float Value;
}
[DisallowMultipleComponent]
public class MovementSpeedComponent : ComponentDataWrapper<MovementSpeed> { }
