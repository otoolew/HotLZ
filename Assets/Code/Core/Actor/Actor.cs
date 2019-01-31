using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventActorRemoved : UnityEvent<bool> { }
public abstract class Actor : MonoBehaviour
{
    #region Fields and Properties
    public abstract FactionAlignment Faction { get; set; }
    #endregion
    #region Events and Handlers
    public abstract event Action<Actor> OnActorRemoved;
    #endregion
}
