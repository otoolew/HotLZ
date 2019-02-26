using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents
{
    [Serializable] public class FadeComplete : UnityEvent<bool> { }
    [Serializable] public class SceneChangeComplete : UnityEvent<bool> { }
    [Serializable] public class PlayerDeath : UnityEvent<bool> { }
    [Serializable] public class TargetableDeath : UnityEvent<Targetable> { }
}
