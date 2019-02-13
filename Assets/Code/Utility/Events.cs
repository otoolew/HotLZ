// ----------------------------------------------------------------------------
//  William O'Toole 
//  Project: HotLZ
//  SEPT 2018
// ----------------------------------------------------------------------------
using System;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [Serializable] public class FadeComplete : UnityEvent<bool> { }
    [Serializable] public class SceneChangeComplete : UnityEvent<bool> { }
    [Serializable] public class PlayerDeath : UnityEvent<bool> { }
    [Serializable] public class InventorySlotSwap : UnityEvent<InventorySlot, InventorySlot> { }
    [Serializable] public class AcquiredTarget : UnityEvent<UnitActor> { }
    [Serializable] public class LostTarget : UnityEvent { }
    [Serializable] public class HitZoneHit : UnityEvent<int> { }
    [Serializable] public class UnitActorHit : UnityEvent<UnitActor> { }
}
