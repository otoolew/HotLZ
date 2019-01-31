using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventActorEnteredRange : UnityEvent<Actor> { }
[Serializable] public class EventActorExitedRange : UnityEvent<Actor> { }

[Serializable] public class EventAcquiredActor : UnityEvent<Actor> { }
[Serializable] public class EventLostActor : UnityEvent { }

public abstract class ActorTracking : MonoBehaviour
{
    /// <summary>
    /// Fired by the agents died event or when the current target moves out of range,
    /// Fires the lostTarget event.
    /// </summary>
    protected abstract void OnActorRemoved(Actor actor);

}