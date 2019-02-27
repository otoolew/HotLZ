using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContestableTerritory : MonoBehaviour
{
    public abstract FactionAlignment CurrentFaction { get; set; }
    public abstract DefensePosition[] DefensePositions { get; }
    public abstract void UpdateFactionOwnership();
    public abstract DefensePosition ClosestDefensePosition(Transform goTranform);
    public abstract bool FindFoxhole(Soldier soldier);
}
