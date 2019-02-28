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

    [SerializeField] private List<PositionAssignment> territoryPositionList;   
    public List<PositionAssignment> TerritoryPositionList { get => territoryPositionList; }

    public RallyPoint blueExit;
    public RallyPoint redExit;

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
        for (int i = 0; i < DefensePositions.Length; i++)
        {
            for (int j = 0; j < DefensePositions[i].positionAssignments.Length; j++)
            {
                territoryPositionList.Add(DefensePositions[i].positionAssignments[j]);
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
            Debug.Log(soldier.name + " Assigned to Defense Position");
            return;
        }
        HeadToExitRally(soldier);
        Debug.Log(soldier.name + " is heading to the " + name + " exit!");
    }

    public bool RequestPositionAssignment(Soldier soldier)
    {
        for (int i = TerritoryPositionList.Count - 1; i >= 0; i--)
        {
            if ((!TerritoryPositionList[i].PositionClaimed) && (TerritoryPositionList[i].FactionAlignment == soldier.FactionComponent.Alignment))
            {
                TerritoryPositionList[i].AssignPosition(soldier);
                soldier.NavigationAgent.GoToPosition(TerritoryPositionList[i].transform.position);
                return true;
            }
        }
        return false;
    }

    public void HeadToExitRally(Soldier soldier)
    {
        switch (soldier.FactionComponent.Alignment.factionAlignmentType)
        {
            case Enums.FactionAlignmentType.NEUTRAL:
                break;
            case Enums.FactionAlignmentType.BLUE:
                soldier.NavigationAgent.GoToPosition(blueExit.transform.position);
                break;
            case Enums.FactionAlignmentType.RED:
                soldier.NavigationAgent.GoToPosition(redExit.transform.position);
                break;
            default:
                break;
        }
    }
    public void UpdateFactionOwnership()
    {
        blueFactionDefense = 0;
        redFactionDefense = 0;
        for (int i = 0; i < defensePositions.Length; i++)
        {
            if (defensePositions[i].FactionAlignment.factionName == "Blue")
            {
                blueFactionDefense++;
            }
            if (defensePositions[i].FactionAlignment.factionName == "Red")
            {
                redFactionDefense++;
            }
        }
        //if (blueFactionDefense > redFactionDefense)
        //OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.BlueFaction);
        //if (redFactionDefense > blueFactionDefense)
        //OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.RedFaction);
        //if (blueFactionDefense == redFactionDefense)
        //OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.NeutralFaction);
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
