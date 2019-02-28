using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactionComponent))]
public abstract class RallyPoint : MonoBehaviour
{
    public abstract FactionComponent FactionComponent { get; set; }
    public abstract Territory ResidingTerritory { get; set; }
    public abstract int UnitRallyMax { get; set; }
    public abstract void RallyUnit(Soldier soldier);
    public abstract void DeploySquad();
}
