using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionStatus
{
    [Serializable] public enum CollectorStatus { DEPLOYING, IDLE, FETCHING, LOADING, DELIVERING, UNLOADING }
    [Serializable] public enum TroopStatus { DEPLOYING, IDLE, MARCHING, ATTACKING, DEFENDING }
}
