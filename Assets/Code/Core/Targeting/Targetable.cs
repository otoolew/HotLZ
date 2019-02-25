using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targetable : MonoBehaviour
{
    public abstract FactionAlignment FactionAlignment { get; set; }
    public abstract HealthComponent HealthComponent { get; set; }
    public Action<Targetable> removed { get; internal set; }
    //public abstract event Action<Targetable> targetRemoved;
}
