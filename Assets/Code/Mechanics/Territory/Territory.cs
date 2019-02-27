using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Territory : ContestableTerritory
{
    [SerializeField] private FactionAlignment currentFaction;
    public override FactionAlignment CurrentFaction { get => currentFaction; set => currentFaction = value; }

    [SerializeField] private DefensePosition[] defensePositions;
    public override DefensePosition[] DefensePositions { get => defensePositions; }

    [SerializeField] private List<Foxhole> territoryFoxholeList;   
    public List<Foxhole> TerritoryFoxholeList { get => territoryFoxholeList; }

    public RallyPoint blueEntrance;
    public RallyPoint blueExit;

    public RallyPoint redEntrance;
    public RallyPoint redExit;

    public int blueFactionDefense;
    public int redFactionDefense;

    #region Events and Handlers

    public void OnTerritoryOwnerChange(FactionAlignment newfaction)
    {
        CurrentFaction = newfaction;
    }
    #endregion  

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DefensePositions.Length; i++)
        {
            for (int j = 0; j < DefensePositions[i].foxholes.Length; j++)
            {
                territoryFoxholeList.Add(DefensePositions[i].foxholes[j]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override bool FindFoxhole(Soldier soldier)
    {
        for (int i = TerritoryFoxholeList.Count - 1; i >= 0; i--)
        {
            if (TerritoryFoxholeList[i].CurrentOccupant == null)
            {
                //TerritoryFoxholeList[i].ClaimFoxhole(soldier);
                soldier.NavigationAgent.GoToPosition(TerritoryFoxholeList[i].transform.position);
                return true;
            }
        }
        return false;
    }
    public void HeadToExitRally(Soldier soldier)
    {
        switch (soldier.FactionAlignment.factionAlignmentType)
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
    public override void UpdateFactionOwnership()
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

    public override DefensePosition ClosestDefensePosition(Transform goTranform)
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
