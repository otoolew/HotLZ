using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitActorFactory
{
    public static GameObject InstantiatePrefab(UnitActorSchematic unitActorSchematic)
    {
        GameObject unitActor = GameObject.Instantiate(unitActorSchematic.actorPrefab);
        unitActor.GetComponent<UnitActor>().Faction = unitActorSchematic.faction;
        unitActor.GetComponent<UnitActor>().Faction.uniform.ChangeUniform(unitActor);
   
        //unitActor.transform.Find("Model").transform.Find("Head").GetComponent<SkinnedMeshRenderer>().material = unitActorSchematic.material;
        //unitActor.transform.Find("Model").transform.Find("Body").GetComponent<SkinnedMeshRenderer>().material = unitActorSchematic.material;
        return unitActor;
    }
}
