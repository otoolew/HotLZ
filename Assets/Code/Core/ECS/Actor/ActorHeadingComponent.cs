using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public struct Heading : IComponentData
{
    public float3 Value;

    public Heading(float3 heading)
    {
        Value = heading;
    }
}
[DisallowMultipleComponent]
public class ActorHeadingComponent : ComponentDataWrapper<Heading> { }
