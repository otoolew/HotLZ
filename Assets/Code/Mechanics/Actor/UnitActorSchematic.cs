using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newUnitActor", menuName = "Factory/UnitActor")]
public class UnitActorSchematic : ScriptableObject
{
    public GameObject actorPrefab;
    public FactionAlignment faction;
}
