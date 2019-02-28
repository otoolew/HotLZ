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

    [SerializeField] private Territory nextTerritory;
    public Territory NextTerritory { get => nextTerritory; set => nextTerritory = value; }

    [SerializeField] private RallyPoint[] rallyPoints;
    public RallyPoint[] RallyPoints { get => rallyPoints;}

    public int blueFactionDefense;
    public int redFactionDefense;

    #region Events and Handlers

    public void OnTerritoryOwnerChange(FactionAlignment newfaction)
    {
        ControllingFaction = newfaction;
    }
    #endregion  

    // Start is called before the first frame update
    void Start()
    {
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
    public void DutyRequest(Soldier soldier)
    {
        if(RequestPositionAssignment(soldier))
        {
            //Debug.Log(soldier.name + " Assigned to Defense Position");
            return;
        }
        RequestRallyPoint(soldier);
    }

    public bool RequestPositionAssignment(Soldier soldier)
    {
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

    public void RequestRallyPoint(Soldier soldier)
    {
        Debug.Log(soldier.name + "Requesting Rally at " + name);
        for (int i = 0; i < rallyPoints.Length; i++)
        {
            if(rallyPoints[i].FactionComponent.Alignment == soldier.FactionComponent.Alignment)
            {
                soldier.NavigationAgent.GoToPosition(rallyPoints[i].transform.position);
                return;
            }
        }
        if(NextTerritory == null)
        {
            Debug.Log("No Territoies left");
            return;
        }
        NextTerritory.RequestRallyPoint(soldier);
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
