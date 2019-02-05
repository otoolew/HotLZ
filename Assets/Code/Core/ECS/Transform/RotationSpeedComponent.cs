using System;
using Unity.Entities;
using UnityEngine;

// Serializable attribute is for editor support.
[Serializable]
public struct RotationSpeed : IComponentData
{
    public float Value;
}
// ComponentDataWrapper is for creating a Monobehaviour representation of this component (for editor support).
[DisallowMultipleComponent]
public class RotationSpeedComponent : ComponentDataWrapper<RotationSpeed> { }
