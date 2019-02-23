using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class EventTerritoryOwnerChange : UnityEvent<FactionAlignment> { }

public class Territory : MonoBehaviour
{
    [SerializeField] private FactionAlignment currentFaction;
    public FactionAlignment CurrentFaction { get => currentFaction; set => currentFaction = value; }
 
    public EntryPoint blueEntrance;
    public EntryPoint redEntrance;
    public List<DefensePosition> availableDefensePositions = new List<DefensePosition>();
    public DefensePosition[] defensePositions;
    public int blueFactionDefense;
    public int redFactionDefense;

    #region Events and Handlers
    public EventTerritoryOwnerChange OnTerritoryOwnerChange;

    public void HandleDefensePositionTaken(DefensePosition defensePosition)
    {
        Debug.Log("Defense POS Taken");
        for (int i = 0; i < availableDefensePositions.Count; i++)
        {
            if (availableDefensePositions[i] == defensePosition)
            {
                availableDefensePositions.RemoveAt(i);
                break;
            }
        }
        UpdateFactionOwnership();
    }
    public void HandleDefensePositionReleased(DefensePosition defensePosition)
    {
        Debug.Log("Defense POS Released");
        availableDefensePositions.Add(defensePosition);
        UpdateFactionOwnership(); // Inefficient
    }
    public void HandleTerritoryOwnerChange(FactionAlignment newfaction)
    {
        CurrentFaction = newfaction;
    }
    #endregion  

    // Start is called before the first frame update
    void Start()
    {
        defensePositions = GetComponentsInChildren<DefensePosition>();
        for (int i = 0; i < defensePositions.Length; i++)
        {
            defensePositions[i].OnDefensePositionTaken.AddListener(HandleDefensePositionTaken);
            defensePositions[i].OnDefensePositionReleased.AddListener(HandleDefensePositionReleased);
            availableDefensePositions.Add(defensePositions[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFactionOwnership()
    {
        blueFactionDefense = 0;
        redFactionDefense = 0;
        for (int i = 0; i < defensePositions.Length; i++)
        { 
            if(defensePositions[i].IsOccupied)
            {
                if (defensePositions[i].Faction.factionName == "Blue")
                {
                    blueFactionDefense++;
                }
                if (defensePositions[i].Faction.factionName == "Red")
                {
                    redFactionDefense++;
                }
            }
        }
        if (blueFactionDefense > redFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.BlueFaction);
        if (redFactionDefense > blueFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.RedFaction);
        if (blueFactionDefense == redFactionDefense)
            OnTerritoryOwnerChange.Invoke(FactionManager.Instance.FactionProvider.NeutralFaction);
    }

    public bool FindDefensePosition(Soldier unitActor)
    {
        foreach (var defensePosition in availableDefensePositions)
        {
            if (!defensePosition.IsOccupied)
            {
                unitActor.GetComponent<NavigationAgent>().GoToPosition(defensePosition.transform.position);
                return true;
            }               
        }
        //if (availableDefensePositions.Count > 0)
        //{
        //    for (int i = 0; i < availableDefensePositions.Count; i++)
        //    {
        //        if (!defensePositions[i].IsOccupied)
        //        {
        //            unitActor.GetComponent<NavigationAgent>().GoToPosition(defensePositions[i].transform.position);
        //            return true;
        //        }           
        //    }
        //}
        if(unitActor.Faction == blueEntrance.Faction)
        {
            unitActor.GetComponent<NavigationAgent>().GoToPosition(blueEntrance.RallyPoint.transform.position);
            return true;
        }
        if (unitActor.Faction == redEntrance.Faction)
        {
            unitActor.GetComponent<NavigationAgent>().GoToPosition(redEntrance.RallyPoint.transform.position);
            return true;
        }
        return false;
    }

}
