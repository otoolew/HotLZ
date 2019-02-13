﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{
    [Serializable] public enum WayPointType { LINKED, RANDOM, LOOP }
    [Serializable] public enum StarshipType { FIGHTER, DEFENDER, MINER }
    [Serializable] public enum PartComponentType { WEAPON, HULL, ENGINE, UTILITY }
    [Serializable] public enum ItemType { NONE, COLLECTABLE, EQUIPPABLE, MISC}
    [Serializable] public enum NavStatus { INROUTE, ARRIVED }
    [Serializable] public enum RotorAxisRotation { X, Y, Z }
    [Serializable] public enum UnitType { SOLDIER, HELICOPTER, VEHICLE }
    [Serializable] public enum SoldierType { RIFLE, ROCKET, MEDIC }
    [Serializable] public enum TowerType { RIFLE, ROCKET, MEDIC }

}
