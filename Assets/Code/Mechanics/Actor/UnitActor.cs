using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(HealthController))]
public abstract class UnitActor : Actor
{
    public abstract Enums.UnitType UnitType { get; set; }
    public abstract HealthController HealthController { get; set; }
    public abstract bool Dead { get; set; }
    public abstract bool Pooled { get; set; }

    #region Events and Handlers
    //public event Action<UnitActor> removed;
    //public abstract event Action<Actor> OnActorRemoved;

    #endregion

    public abstract void UnitActorDeath();


}
