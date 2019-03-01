using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryCheckPoint : MonoBehaviour
{
    [SerializeField] private FactionComponent factionComponent;
    public FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private Territory residingTerritory;
    public Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField] private TerritoryCheckPoint nextCheckPoint;
    public TerritoryCheckPoint NextCheckPoint { get => nextCheckPoint; set => nextCheckPoint = value; }

    [SerializeField] private bool isEntryPoint;
    public bool IsEntryPoint { get => isEntryPoint; set => isEntryPoint = value; }

    [SerializeField] private bool isExitPoint;
    public bool IsExitPoint { get => isExitPoint; set => isExitPoint = value; }

    [SerializeField] private Transform endPoint;
    public Transform EndPoint { get => endPoint; set => endPoint = value; }

    private void Start()
    {
        residingTerritory = GetComponentInParent<Territory>();
        // TODO: Finish Load Setup
        //foreach (var checkPoint in residingTerritory.CheckPointList)
        //{
        //    if (isEntryPoint && (checkPoint.FactionComponent.Alignment == FactionComponent.Alignment))
        //    {
        //        nextCheckPoint = checkPoint;
        //    }
        //    if (isExitPoint && (checkPoint.FactionComponent.Alignment == FactionComponent.Alignment))
        //    {
        //        nextCheckPoint = residingTerritory.FindNextTerritoryEntryPoint(factionComponent);
        //    }
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        Soldier soldier = other.GetComponentInParent<Soldier>();
        if (soldier == null)
            return;

        soldier.CurrentTerritory = residingTerritory;

        if (soldier.FactionComponent.Alignment != FactionComponent.Alignment)
            return;

        if (residingTerritory.RequestPositionAssignment(soldier))
            return;

        if (nextCheckPoint == null)
        {
            MoveToEndPoint(soldier);
            return;
        }
        MoveToNextCheckPoint(soldier);        
    }

    public void MoveToNextCheckPoint(Soldier soldier)
    {
        soldier.NavigationAgent.GoToPosition(nextCheckPoint.transform.position);        
    }

    public void MoveToEndPoint(Soldier soldier)
    {
        soldier.NavigationAgent.GoToPosition(endPoint.position);
    }
}
