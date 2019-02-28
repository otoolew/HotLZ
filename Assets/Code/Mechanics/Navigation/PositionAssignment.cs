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

    [SerializeField] private bool soldierArrived;
    public bool SoldierArrived { get => soldierArrived; set => soldierArrived = value; }

    [SerializeField] private bool positionClaimed;
    public bool PositionClaimed { get => positionClaimed; set => positionClaimed = value; }

    public event Action<PositionAssignment> Assigned;
    public event Action<PositionAssignment> Unassigned;
    private void Update()
    {
        // Can be Event or check could be removed
        if(positionClaimed)
        {
            soldierArrived = HasSoldierArrived();
        }

    }
    public void AssignPosition(Targetable soldier)
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
        //Unassigned.Invoke(this);
    }
    public void OnAssignedSoldierDeath(Targetable soldier)
    {
        soldier.removed -= OnAssignedSoldierDeath;
        UnassignPosition();
    }
    public bool HasSoldierArrived()
    {
        if(assignedSoldier == null || assignedSoldier.IsDead)
        {
            Debug.Log("Soldier is MIA!");
            return false;
        }

        float distance = Vector3.Distance(transform.position, assignedSoldier.transform.position);
        if(distance <= 1f)
        {
            return true;
        }
        return false;
    }
}
