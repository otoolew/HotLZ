using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoldierFactory
{
    public static GameObject InstantiatePrefab(SoldierSchematic soldierSchematic)
    {
        GameObject soldier = GameObject.Instantiate(soldierSchematic.actorPrefab);
        soldier.GetComponent<FactionComponent>().Alignment = soldierSchematic.factionAlignment;
        soldier.GetComponent<FactionComponent>().Alignment.uniform.ChangeUniform(soldier);
        soldier.GetComponentInChildren<AITargetingComponent>().FactionAlignment = soldierSchematic.factionAlignment;
        return soldier;
    }
}
