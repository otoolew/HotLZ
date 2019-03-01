using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FactionComponent))]
public class RallyPoint : MonoBehaviour
{
    [SerializeField] private FactionComponent factionComponent;
    public FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private Territory residingTerritory;
    public Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField] private PositionAssignment[] rallyPositions;
    public PositionAssignment[] RallyPositions { get => rallyPositions; private set => rallyPositions = value; }

    [SerializeField] private Transform endPosition;
    public Transform EndPosition { get => endPosition; private set => endPosition = value; }

    private void Start()
    {
        factionComponent = GetComponent<FactionComponent>();
        residingTerritory = GetComponentInParent<Territory>();
        rallyPositions = GetComponentsInChildren<PositionAssignment>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            if (soldier == null)
                return;
            if (soldier.UnitType != Enums.UnitType.SOLDIER || soldier.FactionComponent.Alignment != factionComponent.Alignment)
                return;
            RallyUnit(soldier);
        }
    }
    public void RallyUnit(Soldier soldier)
    {
        soldier.GetComponent<NavigationAgent>().GoToPosition(endPosition.position);     
    }

}
