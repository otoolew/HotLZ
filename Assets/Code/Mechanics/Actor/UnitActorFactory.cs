using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitActorFactory
{
    public static GameObject InstantiatePrefab(UnitActorSchematic unitActorSchematic)
    {
        GameObject unitActor = GameObject.Instantiate(unitActorSchematic.actorPrefab);
        unitActor.GetComponent<UnitActor>().FactionAlignment = unitActorSchematic.faction;
        unitActor.GetComponent<UnitActor>().FactionAlignment.uniform.ChangeUniform(unitActor);
  
        return unitActor;
    }
}
