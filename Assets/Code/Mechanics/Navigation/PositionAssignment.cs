using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAssignment : MonoBehaviour
{
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private Targetable assignedSoldier;
    public Targetable AssignedSoldier { get => assignedSoldier; set => assignedSoldier = value; }

    [SerializeField] private bool positionClaimed;
    public bool PositionClaimed { get => positionClaimed; set => positionClaimed = value; }

    public event Action<PositionAssignment> Assigned;
    public event Action<PositionAssignment> Unassigned;

    public void AssignPosition(Targetable soldier)
    {
        soldier.removed += OnAssignedSoldierDeath;
        assignedSoldier = soldier;
        Assigned.Invoke(this);
    }
    public void UnassignPosition()
    {
        assignedSoldier = null;
        positionClaimed = false;
        Unassigned.Invoke(this);
    }
    public void OnAssignedSoldierDeath(Targetable soldier)
    {
        soldier.removed -= OnAssignedSoldierDeath;
        UnassignPosition();
    }
}
