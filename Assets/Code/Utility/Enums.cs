using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    [Serializable] public enum WayPointType { LINKED, RANDOM, LOOP }
    [Serializable] public enum ItemType { NONE, COLLECTABLE, EQUIPPABLE, MISC}
    [Serializable] public enum NavStatus { INROUTE, ARRIVED }
    [Serializable] public enum RotorAxisRotation { X, Y, Z }
    [Serializable] public enum UnitType { SOLDIER, HELICOPTER, VEHICLE, TOWER }
    [Serializable] public enum DefenseTowerType { RIFLE, ROCKET, MEDIC }
    [Serializable] public enum DefenseTowerState { OCCUPIED, UNOCCUPIED, DESTROYED }
    [Serializable] public enum FactionAlignmentType { NEUTRAL, BLUE, RED }

}
