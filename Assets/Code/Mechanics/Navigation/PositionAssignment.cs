using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionAssignment : MonoBehaviour
{
    [SerializeField] private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    [SerializeField] private Territory residingTerritory;
    public Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField] private Soldier assignedSoldier;
    public Soldier AssignedSoldier { get => assignedSoldier; set => assignedSoldier = value; }

    [SerializeField] private bool soldierArrived;
    public bool SoldierArrived { get => soldierArrived; set => soldierArrived = value; }

    [SerializeField] private bool positionClaimed;
    public bool PositionClaimed { get => positionClaimed; set => positionClaimed = value; }

    public event Action<PositionAssignment> Assigned;
    public event Action<PositionAssignment> Unassigned;

    private void Start()
    {
        residingTerritory = GetComponentInParent<Territory>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if (soldier == null)
                return;
            if (soldier.FactionComponent.Alignment != factionAlignment)
                return;
            if (positionClaimed && (assignedSoldier == soldier))
            {
                soldierArrived = true;
                return;
            }
            if (assignedSoldier != soldier)
            {
                if (residingTerritory.RequestPositionAssignment(assignedSoldier))
                {
                    UnassignPosition();
                    AssignPosition(soldier);
                    soldierArrived = true;
                    soldier.CurrentPositionAssignment = this;
                    return;
                }

                if (residingTerritory.RequestCheckPointLocation(soldier))
                    Debug.Log("TODO: NO AVAILABLE CHECKPOINT");
            }
        }
    }
    public void AssignPosition(Soldier soldier)
    {        
        soldier.removed += OnAssignedSoldierDeath;
        assignedSoldier = soldier;
        assignedSoldier.GetComponent<Soldier>().CurrentPositionAssignment = this;
        positionClaimed = true;
        //Assigned?.Invoke(this);
    }
    public void UnassignPosition()
    {
        assignedSoldier.GetComponent<Soldier>().CurrentPositionAssignment = null;
        assignedSoldier = null;
        positionClaimed = false;
        soldierArrived = false;
        GetComponent<SphereCollider>().enabled = true;
        //Unassigned.Invoke(this);
    }
    public void OnAssignedSoldierDeath(Targetable soldier)
    {
        soldier.removed -= OnAssignedSoldierDeath;
        UnassignPosition();
    }
}
