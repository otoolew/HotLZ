using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class UnitActor : Targetable
{
    public abstract Enums.UnitType UnitType { get; set; }
    public abstract bool Dead { get; set; }
    public abstract bool Pooled { get; set; }

    #region Events and Handlers
    public abstract event Action<UnitActor> removed;
    //public abstract event Action<Actor> OnActorRemoved;

    #endregion
    
    public abstract void UnitActorDeath();



}
