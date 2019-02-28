using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryRallyPoint : RallyPoint
{
    [SerializeField] private FactionComponent factionComponent;
    public override FactionComponent FactionComponent { get => factionComponent; set => factionComponent = value; }

    [SerializeField] private Territory residingTerritory;
    public override Territory ResidingTerritory { get => residingTerritory; set => residingTerritory = value; }

    [SerializeField] private Territory nextTerritory;
    public Territory NextTerritory { get => nextTerritory; set => nextTerritory = value; }

    [SerializeField] private int unitRallyMax;
    public override int UnitRallyMax { get => unitRallyMax; set => unitRallyMax = value; }

    [SerializeField] private int unitRallyCount;
    public int UnitRallyCount { get => unitRallyCount; set => unitRallyCount = value; }

    [SerializeField] private PositionAssignment[] rallyPositions;
    public PositionAssignment[] RallyPositions { get => rallyPositions; private set => rallyPositions = value; }

    public Stack<Soldier> soldierStack = new Stack<Soldier>();

    private void Start()
    {
        factionComponent = GetComponent<FactionComponent>();
        residingTerritory = GetComponentInParent<Territory>();
        rallyPositions = GetComponentsInChildren<PositionAssignment>();

        //foreach (PositionAssignment position in residingTerritory.PositionAssignmentList)
        //{
        //    if (position.FactionAlignment == factionComponent.Alignment)
        //        rallyPositionList.Add(position);
        //}
        unitRallyMax = rallyPositions.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Targetable"))
        {
            Soldier soldier = other.GetComponentInParent<Soldier>();
            Debug.Log(soldier);
            if (soldier == null)
                return;
            if (soldier.UnitType != Enums.UnitType.SOLDIER || soldier.FactionComponent.Alignment != factionComponent.Alignment)
                return;
            RallyUnit(soldier);
        }
    }
    public override void RallyUnit(Soldier soldier)
    {
        if (unitRallyCount < unitRallyMax)
        {
            soldierStack.Push(soldier);
            unitRallyCount = soldierStack.Count;
            int positionIndex = unitRallyCount;
            soldier.GetComponent<NavigationAgent>().GoToPosition(rallyPositions[positionIndex - 1].transform.position);
            if (unitRallyCount == unitRallyMax)
                DeploySquad();
        }

        //for (int i = 0; i < rallyPositions.Length; i++)
        //{
        //    if (!rallyPositions[i].PositionClaimed)
        //    {
        //        rallyPositions[i].AssignPosition(soldier);
        //        soldier.NavigationAgent.GoToPosition(rallyPositions[i].transform.position);
        //        soldierQueue.Enqueue(soldier);
        //        break;
        //    }
        //}
        //if (soldierQueue.Count >= unitRallyMax)
        //{
        //    DeploySquad();
        //    //unitRallyCount = 0;
        //}
    }

    public override void DeploySquad()
    {

        for (int i = 1; i <= unitRallyCount; i++)
        {
            try
            {
                NextTerritory.DutyRequest(soldierStack.Pop());
                unitRallyCount = soldierStack.Count;
            }
            catch (System.NullReferenceException)
            {
                Debug.Log(gameObject.name + " Rally Throw Catch");
            }
        }
        unitRallyCount = soldierStack.Count;
        //int deployCount = unitRallyMax;
        //while (deployCount > 0)
        //{
        //    try
        //    {
        //        Soldier soldier = soldierQueue.Dequeue();
        //        soldier.CurrentPositionAssignment.UnassignPosition();
        //        ResidingTerritory.NextTerritory.DutyRequest(soldier);
        //        deployCount--;
        //        Debug.Log("Deploying " + deployCount);
        //    }
        //    catch (System.IndexOutOfRangeException)
        //    {
        //        Debug.Log("Territory Rally Stack Out of Range Error.");
        //    }
        //}

    }
}
