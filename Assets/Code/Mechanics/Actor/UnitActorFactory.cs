using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitActorFactory
{
    public static UnitActor InstantiatePrefab(UnitActorSchematic unitActorSchematic)
    {
        UnitActor unitActor = GameObject.Instantiate(unitActorSchematic.actorPrefab);
        unitActor.GetComponent<UnitActor>().Faction = unitActorSchematic.faction;
        unitActor.transform.Find("Model").transform.Find("Head").GetComponent<SkinnedMeshRenderer>().material = unitActorSchematic.material;
        unitActor.transform.Find("Model").transform.Find("Body").GetComponent<SkinnedMeshRenderer>().material = unitActorSchematic.material;
        return unitActor;
    }
}
