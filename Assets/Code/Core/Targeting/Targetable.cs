using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Targetable : MonoBehaviour
{
    public abstract FactionAlignment Faction { get; set; }
    public abstract HealthComponent HealthComponent { get; set; }
    public abstract event Action<Targetable> OnTargetRemoved;
}
