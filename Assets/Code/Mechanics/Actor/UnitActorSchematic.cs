using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newUnitActor", menuName = "Factory/UnitActor")]
public class UnitActorSchematic : ScriptableObject
{
    public UnitActor actorPrefab;
    public FactionAlignment faction;
    public Material material;
}
