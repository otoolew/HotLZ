using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newUnitActor", menuName = "Factory/Soldier")]
public class SoldierSchematic : ScriptableObject
{
    public GameObject actorPrefab;
    public FactionAlignment factionAlignment;
}
