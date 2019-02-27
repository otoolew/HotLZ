using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoldierFactory
{
    public static GameObject InstantiatePrefab(SoldierSchematic soldierSchematic)
    {
        GameObject soldier = GameObject.Instantiate(soldierSchematic.actorPrefab);
        soldier.GetComponent<FactionComponent>().FactionAlignment = soldierSchematic.factionAlignment;
        soldier.GetComponent<Soldier>().FactionAlignment = soldierSchematic.factionAlignment;
        soldier.GetComponentInChildren<AITargetingComponent>().FactionAlignment = soldierSchematic.factionAlignment;
        soldier.GetComponent<Soldier>().FactionAlignment.uniform.ChangeUniform(soldier);
        return soldier;
    }
}
