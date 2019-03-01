using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Territory : MonoBehaviour
{
    [SerializeField] private FactionAlignment controllingFaction;
    public FactionAlignment ControllingFaction { get => controllingFaction; set => controllingFaction = value; }

    [SerializeField] private DefensePosition[] defensePositions;
    public DefensePosition[] DefensePositions { get => defensePositions; }

    [SerializeField] private List<PositionAssignment> positionAssignmentList;   
    public List<PositionAssignment> PositionAssignmentList { get => positionAssignmentList; }

    [SerializeField] private Territory nextBlueTerritory;
    public Territory NextBlueTerritory { get => nextBlueTerritory; set => nextBlueTerritory = value; }

    [SerializeField] private Territory nextRedTerritory;
    public Territory NextRedTerritory { get => nextRedTerritory; set => nextRedTerritory = value; }

    [SerializeField] private List<TerritoryCheckPoint> checkPointList;
    public List<TerritoryCheckPoint> CheckPointList { get => checkPointList; private set => checkPointList = value; }

    public int blueFactionDefense;
    public int redFactionDefense;

    #region Events and Handlers

    public void OnTerritoryOwnerChange(FactionAlignment newfaction)
    {
        ControllingFaction = newfaction;
    }
    #endregion  

    private void SetUpCheckPoints()
    {
        TerritoryCheckPoint[] checkPoints = GetComponentsInChildren<TerritoryCheckPoint>();
        if(checkPoints.Length > 0)
        {
            for (int i = 0; i < checkPoints.Length; i++)
            {
                checkPointList.Add(checkPoints[i]);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpCheckPoints();
        defensePositions = GetComponentsInChildren<DefensePosition>();
        for (int i = 0; i < DefensePositions.Length; i++)
        {
            for (int j = 0; j < DefensePositions[i].positionAssignments.Length; j++)
            {
                positionAssignmentList.Add(DefensePositions[i].positionAssignments[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool RequestPositionAssignment(Soldier soldier)
    {
        if (soldier == null)
            return false;
        for (int i = PositionAssignmentList.Count - 1; i >= 0; i--)
        {
            if ((!PositionAssignmentList[i].PositionClaimed) && (PositionAssignmentList[i].FactionAlignment == soldier.FactionComponent.Alignment))
            {
                PositionAssignmentList[i].AssignPosition(soldier);
                soldier.NavigationAgent.GoToPosition(PositionAssignmentList[i].transform.position);
                return true;
            }
        }
        return false;
    }
    public TerritoryCheckPoint FindNextTerritoryEntryPoint(FactionComponent faction)
    {

        for (int i = CheckPointList.Count - 1; i >= 0; i--)
        {
            if (checkPointList[i].IsEntryPoint && checkPointList[i].FactionComponent.Alignment == faction.Alignment)
            {
                return CheckPointList[i];
            }
        }
        return null;
    }
    public bool RequestCheckPointLocation(Soldier soldier)
    {
        for (int i = CheckPointList.Count - 1; i >= 0; i--)
        {
            if (checkPointList[i].IsExitPoint && checkPointList[i].FactionComponent.Alignment == soldier.FactionComponent.Alignment)
            {
                soldier.NavigationAgent.GoToPosition(checkPointList[i].transform.position);
                return true;
            }               
        }
        return false;
    }
    public void UpdateFactionOwnership()
    {

    }

    public DefensePosition ClosestDefensePosition(Transform goTranform)
    {
        DefensePosition closestDefensePositionResult = null;
        if (defensePositions.Length > 0)
        {
            float closestDistance = float.MaxValue;

            for (int i = 0; i < defensePositions.Length; i++)
            {
                float distance = Vector3.Distance(goTranform.position, defensePositions[i].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDefensePositionResult = defensePositions[i];
                }
            }
        }
        return closestDefensePositionResult;
    }
}
